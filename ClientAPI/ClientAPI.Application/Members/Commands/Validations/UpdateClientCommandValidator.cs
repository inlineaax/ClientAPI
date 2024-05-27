using ClientAPI.Domain.Abstractions;
using ClientAPI.Infrastructure.RabbitMQ.Exceptions;
using FluentValidation;

namespace ClientAPI.Application.Members.Commands.Validations
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Please ensure you have entered the Name")
              .Length(3, 100).WithMessage("The Name must have between 4 and 100 characters")
              .Matches(@"^(?!.*\s{2})[A-Za-z\s]+$").WithMessage("The Name must contain only letters");

            RuleFor(c => c.Gender)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("The gender must be a valid information");

            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("Please ensure you have entered the Email")
               .EmailAddress().WithMessage("Please ensure you have entered a valid Email")
               .MustAsync(BeUniqueEmail).WithMessage("Email already registered");

            RuleFor(x => x.Id).MustAsync(ClientExists).WithMessage("Client with ID {PropertyValue} does not exist.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.MongoClientRepository.GetClientByEmailAsync(email);
            return client == null;
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
