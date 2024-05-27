using ClientAPI.Application.Members.Queries;
using ClientAPI.Domain.Abstractions;
using ClientAPI.Infrastructure.RabbitMQ.Exceptions;
using FluentValidation;

namespace ClientAPI.Application.Members.Commands.Validations
{
    public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetClientByIdQueryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Id).MustAsync(ClientExists).WithMessage("Client with ID {PropertyValue} does not exist.");
        }

        private async Task<bool> ClientExists(int clientId, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.MongoClientRepository.GetClientByIdAsync(clientId);
            if (client == null)
            {
                throw new ClientNotFoundException(clientId);
            }
            return true;
        }
    }
}
