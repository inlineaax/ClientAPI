using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Domain.Messages;
using MediatR;

namespace ClientAPI.Application.Members.Commands
{
    public sealed class UpdateClientCommand : ClientCommandBase
    {
        public int Id { get; set; }
        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
        {
            private readonly IMessageProducer _messageProducer;

            public UpdateClientCommandHandler(IMessageProducer messageProducer)
            {
                _messageProducer = messageProducer;
            }

            public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
            {
                // Publish message to RabbitMQ
                _messageProducer.SendMessage(new ClientMessage
                {
                    Operation = "Update",
                    Client = new ClientDto
                    {
                        Id = request.Id,
                        Name = request.Name,
                        Gender = request.Gender,
                        Email = request.Email,
                        IsActive = request.IsActive
                    }
                });

                var updatedClient = new Client(request.Name, request.Gender, request.Email, request.IsActive);

                updatedClient.GetType().GetProperty("Id").SetValue(updatedClient, request.Id);


                return updatedClient;
            }
        }
    }
}
