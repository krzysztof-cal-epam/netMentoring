using CatalogService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace RestApi.Messaging
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly RabbitMqSettings _settings;
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqProducer(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;

            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(queue: _settings.QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null).GetAwaiter().GetResult();
        }

        public async Task PublishAsync(string message, CancellationToken cancellationToken)
        {
            var body = Encoding.UTF8.GetBytes(message);

            var props = new BasicProperties
            {
                Persistent = true
            };

            await _channel.BasicPublishAsync("", _settings.QueueName, true, props, body, cancellationToken);

            Console.WriteLine($"[RabbitMQ] Sent: {message}");
        }

        public void Dispose()
        {
            _channel?.CloseAsync().GetAwaiter().GetResult();
            _connection?.CloseAsync().GetAwaiter().GetResult();
        }
    }
}
