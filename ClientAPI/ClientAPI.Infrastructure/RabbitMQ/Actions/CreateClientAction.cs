using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Domain.Messages;

namespace ClientAPI.Infrastructure.RabbitMQ.Actions
{
    public class CreateClientAction : IClientOpeationAction
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientAction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(ClientMessage clientMessage)
        {
            var newClient = new Client(clientMessage.Client.Name, clientMessage.Client.Gender, clientMessage.Client.Email, clientMessage.Client.IsActive);
            await _unitOfWork.ClientRepository.AddClient(newClient);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.MongoClientRepository.AddClientAsync(newClient);
        }
    }
}
