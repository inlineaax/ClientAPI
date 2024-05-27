using ClientAPI.Domain.Messages;

namespace ClientAPI.Infrastructure.RabbitMQ.Actions
{
    public interface IClientOpeationAction
    {
        Task HandleAsync(ClientMessage clientMessage);
    }
}
