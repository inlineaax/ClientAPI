using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ClientAPI.Domain.Messages;
using ClientAPI.Infrastructure.RabbitMQ.Actions;
using ClientAPI.Domain.Abstractions;

namespace ClientAPI.Infrastructure.RabbitMQ
{
    public class ClientConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IServiceProvider _serviceProvider;

        public ClientConsumer(IConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "client_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var clientMessage = JsonConvert.DeserializeObject<ClientMessage>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var handlers = scope.ServiceProvider.GetServices<IClientOpeationAction>();

                    var handler = handlers.FirstOrDefault(h => h.GetType().Name == $"{clientMessage.Operation}ClientAction");

                    await handler.HandleAsync(clientMessage);
                }
            };

            channel.BasicConsume(queue: "client_queue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
