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

        public CartMessageListener(IServiceScopeFactory scopeFactory, IOptions<RabbitMqSettings> options)
        {
            _scopeFactory = scopeFactory;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();

                await _channel.QueueDeclareAsync(queue: "cart-service-queue",
                                                durable: true,
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);

                await _channel.ExchangeDeclareAsync(exchange: "catalog-events", type: ExchangeType.Direct);

                await _channel.QueueBindAsync(queue: "cart-service-queue",
                                            exchange: "catalog-events",
                                            routingKey: "product.updated");

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

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
                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

                                Console.WriteLine($"[CartMessageListener.consumer.ReceivedAsync] Calling cartService.UpdateCartItems at <{DateTime.UtcNow}>");

                                cartService.UpdateCartItems(
                                    productId: productUpdate.ItemId,
                                    updatedName: productUpdate.Name,
                                    updatedPrice: productUpdate.Price
                                );
                                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                            }
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
                                     consumer: consumer);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[CartMessageListener] Execution canceled.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CartMessageListener] Fatal error: {ex.Message}");
                throw;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("[CartMessageListener] Stopping...");
            try
            {
                if (_channel != null && !string.IsNullOrEmpty(_consumerTag))
                {
                    await _channel.BasicCancelAsync(_consumerTag); // Stop consuming messages
                    Console.WriteLine("[CartMessageListener] Consumer canceled.");
                }
                _channel?.CloseAsync(cancellationToken);
                _connection?.CloseAsync(cancellationToken);
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
            base.Dispose();
        }

        private class ProductUpdateMessage
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
    }
}
