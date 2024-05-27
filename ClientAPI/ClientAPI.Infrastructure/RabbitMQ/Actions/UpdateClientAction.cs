using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Messages;

namespace ClientAPI.Infrastructure.RabbitMQ.Actions
{
    public class UpdateClientAction : IClientOpeationAction
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientAction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(ClientMessage clientMessage)
        {
            var client = await _unitOfWork.MongoClientRepository.GetClientByIdAsync(clientMessage.Client.Id);
            client.Update(clientMessage.Client.Name, clientMessage.Client.Gender, clientMessage.Client.Email, clientMessage.Client.IsActive);
            _unitOfWork.ClientRepository.UpdateClient(client);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.MongoClientRepository.UpdateClientAsync(client);
        }
    }
}
