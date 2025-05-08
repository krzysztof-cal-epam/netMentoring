using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RestApi.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly RabbitMqSettings _settings;

        public RabbitMqConsumer(IOptions<RabbitMqSettings> options)
        {
            _settings = options.Value;
        }

        public void StartListening()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            using var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            using var channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

            channel.QueueDeclareAsync(queue: _settings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null).GetAwaiter().GetResult();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"[RabbitMQ] Received: {message}");

                return Task.CompletedTask;
            };

            channel.BasicConsumeAsync(queue: _settings.QueueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("[RabbitMQ] Listening for messages...");
            Console.ReadLine();
        }
    }
}
