using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.DataAccess.Data;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CatalogService.DataAccess.RabbitMQ;
using Microsoft.Data.SqlClient;

namespace CatalogService.Application.BackgroundServices
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitMqSettings _settings;

        public OutboxProcessor(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMqSettings> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                int retries = 5;
                int delayMs = 5000;

                while (!stoppingToken.IsCancellationRequested)
                {
                    for (int attempt = 1; attempt <= retries; attempt++)
                    {
                        try
                        {
                            using var scope = _serviceScopeFactory.CreateScope();
                            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

                            var unprocessedEvents = await dbContext.Outbox
                                .Where(e => !e.IsProcessed)
                                .ToListAsync(stoppingToken);

                            if (unprocessedEvents.Count == 0)
                            {
                                await Task.Delay(delayMs, stoppingToken);
                                break;
                            }

                            var factory = new ConnectionFactory
                            {
                                HostName = _settings.HostName,
                                Port = _settings.Port,
                                UserName = _settings.UserName,
                                Password = _settings.Password
                            };

                            using var connection = await factory.CreateConnectionAsync();
                            using var channel = await connection.CreateChannelAsync();

                            await channel.ExchangeDeclareAsync(exchange: "catalog-events", type: ExchangeType.Direct, cancellationToken: stoppingToken);

                            foreach (var outboxEvent in unprocessedEvents)
                            {
                                stoppingToken.ThrowIfCancellationRequested();
                                try
                                {
                                    var payloadBytes = Encoding.UTF8.GetBytes(outboxEvent.Payload);
                                    var props = new BasicProperties
                                    {
                                        Persistent = true
                                    };
                                    await channel.BasicPublishAsync(
                                        exchange: "catalog-events",
                                        routingKey: "product.updated",
                                        mandatory: true,
                                        basicProperties: props,
                                        body: payloadBytes);

                                    Console.WriteLine($"[Outbox] Published Event: {outboxEvent.Payload}");

                                    outboxEvent.IsProcessed = true;
                                    outboxEvent.ProcessedAt = DateTime.UtcNow;

                                    dbContext.Outbox.Update(outboxEvent);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[Outbox] Failed to process event '{outboxEvent.Id}': {ex.Message}");
                                }
                            }

                            await dbContext.SaveChangesAsync(stoppingToken);
                            break; // Success, exit retry loop
                        }
                        catch (SqlException ex) when (attempt < retries)
                        {
                            Console.WriteLine($"[Outbox] SQL connection attempt {attempt} failed: {ex.Message}. Retrying in {delayMs}ms...");
                            await Task.Delay(delayMs, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Outbox] Exception '{ex.Message}'");
                            break; // Non-retryable error
                        }
                    }
                    await Task.Delay(delayMs, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[Outbox] Operation canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Outbox] Fatal error: {ex.Message}");
            }
        }
    }
}
