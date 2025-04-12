using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Events
{
    public class ProductEventConsumer:BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        public ProductEventConsumer()
        {

            var hostName = Environment.GetEnvironmentVariable("hostName");

            var factory = new ConnectionFactory() { HostName = hostName };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(queue: "product_events",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    // Detectar tipo de evento (simplemente por estructura)
                    var product = JsonSerializer.Deserialize<ProductEventsDTO>(message);
                    if (product != null)
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores / retry
                }
            };

            _channel.BasicConsumeAsync(queue: "product_events", autoAck: true, consumer: consumer);
        }
    }
}

