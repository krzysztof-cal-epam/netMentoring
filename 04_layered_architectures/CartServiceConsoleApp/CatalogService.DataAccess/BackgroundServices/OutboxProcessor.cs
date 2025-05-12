using RabbitMQ.Client;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.DataAccess.Data;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CatalogService.DataAccess.RabbitMQ;

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

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

                var unprocessedEvents = await dbContext.Outbox
                    .Where(e => !e.IsProcessed)
                    .ToListAsync(cancellationToken);

                if (unprocessedEvents.Count == 0)
                {
                    await Task.Delay(5000, cancellationToken);
                    continue;
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

                await channel.ExchangeDeclareAsync(exchange: "catalog-events", type: ExchangeType.Direct, cancellationToken: cancellationToken);

                foreach (var outboxEvent in unprocessedEvents)
                {
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

                await dbContext.SaveChangesAsync(cancellationToken);

                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}
