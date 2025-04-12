using System.Text;
using System.Text.Json;
using DomainLayer.Models;
using Infrastructure.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedLibrary.Domain.Repositories;

namespace Infrastructure.EventServices
{
    public class ProductEventConsumer:BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        private  IStockServiceCached _stockService;
        private  IRepositoryWrapper _repo;

        private readonly IServiceProvider _serviceProvider;

        public ProductEventConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;


             var hostName = Environment.GetEnvironmentVariable("hostname");

            var factory = new ConnectionFactory() { HostName = hostName };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(queue: "product_events_queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false);
        }


    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                _repo = scope.ServiceProvider.GetRequiredService<IRepositoryWrapper>();
                _stockService = scope.ServiceProvider.GetRequiredService<IStockServiceCached>();
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);


                try
                {
                    // Detectar tipo de evento (simplemente por estructura)
                    var product = JsonSerializer.Deserialize<ProductEventsDTO>(message);

                    var executed = await validateEventWasExecutedAsync(product.EventId);

                    if (product != null && !executed)
                    {
                        await _stockService.ManageProductEventsInStockAsync(product);
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores / retry
                }
            };

           await  _channel.BasicConsumeAsync(queue: "product_events_queue", autoAck: true, consumer: consumer);
        }

        private async Task<bool> validateEventWasExecutedAsync(Guid eventId)
        {
            var exist = _repo.ProcessEvents.FindByCondition(t => t.Id == eventId);
            if(exist != null)
            {
                return true;
            }
            else
            {
                var pEvent = new ProcessEvents()
                {
                    Id = eventId
                };
                await  _repo.ProcessEvents.CreateAsync(pEvent);
                await _repo.SaveAsync();

            }
            return false;
        }
    }
}

