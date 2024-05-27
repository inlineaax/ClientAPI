using ClientAPI.Domain.Abstractions;
using ClientAPI.Domain.Entities;
using ClientAPI.Domain.Messages;
using MediatR;

namespace ClientAPI.Application.Members.Commands
{
    public sealed class DeleteClientCommand : IRequest<Client>
    {
        public int Id { get; set; }

        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Client>
        {
            private readonly IMessageProducer _messageProducer;

            public DeleteClientCommandHandler(IMessageProducer messageProducer)
            {
                _messageProducer = messageProducer;
            }

            public async Task<Client> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
            {
                // Publish message to RabbitMQ
                _messageProducer.SendMessage(new ClientMessage 
                { 
                    Operation = "Delete", 
                    Client = new ClientDto { Id = request.Id } 
                });

                var client = new Client(request.Id);

                return client;
            }
        }
    }
}
