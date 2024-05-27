namespace ClientAPI.Infrastructure.RabbitMQ.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(int clientId)
            : base($"Client with ID {clientId} not found.")
        {
        }
    }
}
