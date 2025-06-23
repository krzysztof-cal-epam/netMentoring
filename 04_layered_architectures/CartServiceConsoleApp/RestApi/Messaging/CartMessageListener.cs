namespace RestApi.Messaging
{
    using System.Text;
    using System.Text.Json;
    using CatalogService.Application.Interfaces;
    using CatalogService.DataAccess.RabbitMQ;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class CartMessageListener : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly RabbitMqSettings _settings;
        private IConnection _connection;
        private IChannel _channel;
        private string _consumerTag;
        private bool _disposed;

        public CartMessageListener(IServiceScopeFactory scopeFactory, IOptions<RabbitMqSettings> options)
        {
            _scopeFactory = scopeFactory;
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
                            var factory = new ConnectionFactory
                            {
                                HostName = _settings.HostName,
                                Port = _settings.Port,
                                UserName = _settings.UserName,
                                Password = _settings.Password
                            };

                            _connection = await factory.CreateConnectionAsync(stoppingToken);
                            _channel = await _connection.CreateChannelAsync();

                            await _channel.QueueDeclareAsync(queue: "cart-service-queue",
                                                            durable: true,
                                                            exclusive: false,
                                                            autoDelete: false,
                                                            arguments: null,
                                                            cancellationToken: stoppingToken);

                            await _channel.ExchangeDeclareAsync(exchange: "catalog-events",
                                                               type: ExchangeType.Direct,
                                                               cancellationToken: stoppingToken);

                            await _channel.QueueBindAsync(queue: "cart-service-queue",
                                                         exchange: "catalog-events",
                                                         routingKey: "product.updated",
                                                         cancellationToken: stoppingToken);

                            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

                            var consumer = new AsyncEventingBasicConsumer(_channel);

                            consumer.ReceivedAsync += async (model, ea) =>
                            {
                                if (stoppingToken.IsCancellationRequested)
                                {
                                    Console.WriteLine("[CartMessageListener] Cancellation requested, ignoring message.");
                                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                                    return;
                                }

                                var body = ea.Body.ToArray();
                                var message = Encoding.UTF8.GetString(body);

                                Console.WriteLine($"[CartMessageListener.consumer.ReceivedAsync] Received message: {message}");

                                try
                                {
                                    var productUpdate = JsonSerializer.Deserialize<ProductUpdateMessage>(message);

                                    if (productUpdate != null)
                                    {
                                        using var scope = _scopeFactory.CreateScope();
                                        var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

                                        Console.WriteLine($"[CartMessageListener.consumer.ReceivedAsync] Calling cartService.UpdateCartItems at <{DateTime.UtcNow}>");

                                        cartService.UpdateCartItems(
                                            productId: productUpdate.ItemId,
                                            updatedName: productUpdate.Name,
                                            updatedPrice: productUpdate.Price
                                        );
                                        await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                                    }
                                    else
                                    {
                                        Console.WriteLine("[CartMessageListener] Invalid product update message, acknowledging.");
                                        await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[Cart Service] Failed to process message: {ex.Message}");
                                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                                }
                            };

                            _consumerTag = await _channel.BasicConsumeAsync(queue: "cart-service-queue",
                                                                           autoAck: false,
                                                                           consumer: consumer,
                                                                           cancellationToken: stoppingToken);
                            break; // Success, exit retry loop
                        }
                        catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex) when (attempt < retries)
                        {
                            Console.WriteLine($"[CartMessageListener] RabbitMQ connection attempt {attempt} failed: {ex.Message}. Retrying in {delayMs}ms...");
                            await Task.Delay(delayMs, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[CartMessageListener] Fatal error: {ex.Message}");
                            throw;
                        }
                    }
                    await Task.Delay(delayMs, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[CartMessageListener] Execution canceled.");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("[CartMessageListener] Stopping...");
            try
            {
                if (_channel != null && !string.IsNullOrEmpty(_consumerTag))
                {
                    await _channel.BasicCancelAsync(_consumerTag);
                    Console.WriteLine("[CartMessageListener] Consumer canceled.");
                }
                if (_channel != null)
                    await _channel.CloseAsync(cancellationToken);
                if (_connection != null)
                    await _connection.CloseAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CartMessageListener] Error during shutdown: {ex.Message}");
            }
            await base.StopAsync(cancellationToken);
            Console.WriteLine("[CartMessageListener] Stopped.");
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    _channel?.Dispose();
                    _connection?.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CartMessageListener] Error disposing RabbitMQ resources: {ex.Message}");
                }
            }

            _disposed = true;
            base.Dispose();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private class ProductUpdateMessage
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}