using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Messages;

namespace ClientAPI.Infrastructure.RabbitMQ.Actions
{
    public class DeleteClientAction : IClientOpeationAction
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientAction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(ClientMessage clientMessage)
        {
                await _unitOfWork.ClientRepository.DeleteClient(clientMessage.Client.Id);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.MongoClientRepository.DeleteClientAsync(clientMessage.Client.Id);
        }
    }
}
