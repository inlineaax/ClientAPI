using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Domain.Messages;
using MediatR;

namespace ClientAPI.Application.Members.Commands
{
    public class CreateClientCommand : ClientCommandBase
    {
        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
        {
            private readonly IMessageProducer _messageProducer;

            public CreateClientCommandHandler(IMessageProducer messageProducer)
            {
                _messageProducer = messageProducer;
            }
            public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {

                var newClient = new Client(request.Name, request.Gender, request.Email, request.IsActive);

                _messageProducer.SendMessage(new ClientMessage
                {
                    Operation = "Create",
                    Client = new ClientDto
                    {
                        Id = newClient.Id,
                        Name = newClient.Name,
                        Gender = newClient.Gender,
                        Email = newClient.Email,
                        IsActive = newClient.IsActive
                    }
                });

                return newClient;
            }
        }

    }
}
