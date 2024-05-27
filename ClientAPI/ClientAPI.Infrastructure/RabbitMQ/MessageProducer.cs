using ClientAPI.Domain.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ClientAPI.Infrastructure.RabbitMQ
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IConnection _connection;

        public MessageProducer(IConnection connection)
        {
            _connection = connection;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };

            using (var connection = factory.CreateConnection()) { }
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "client_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "", routingKey: "client_queue", basicProperties: null, body: body);
            }
        }
    }
}
