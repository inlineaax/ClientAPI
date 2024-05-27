using ClientAPI.Domain.Entities;
using MediatR;

namespace ClientAPI.Application.Members.Commands
{
    public abstract class ClientCommandBase : IRequest<Client>
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
