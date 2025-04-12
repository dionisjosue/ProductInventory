using System;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Events.EventServices
{
	public class RabbitEventsService:IDisposable
	{
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitEventsService()
        {
            var hostName = Environment.GetEnvironmentVariable("hostName");

            var factory = new ConnectionFactory { HostName = hostName };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.ExchangeDeclareAsync(exchange: "product_events", type: ExchangeType.Fanout);
        }

        public void Dispose()
        {
            _channel?.CloseAsync().GetAwaiter().GetResult(); 
            _channel?.Dispose();
            _connection?.CloseAsync().GetAwaiter().GetResult();
            _connection?.Dispose();
        }

        public  async Task Publish<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await _channel.BasicPublishAsync(exchange: "product_events", routingKey: "", body: body);
        }

    }
}

