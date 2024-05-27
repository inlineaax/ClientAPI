using ClientAPI.Domain.Abstractions;
using FluentValidation;

namespace ClientAPI.Application.Members.Commands.Validations
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateClientCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Please ensure you have entered the Name")
              .Length(3, 100).WithMessage("The Name must have between 3 and 100 characters")
              .Matches(@"^(?!.*\s{2})[A-Za-z\s]+$").WithMessage("The Name must contain only letters");

            RuleFor(c => c.Gender)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("The gender must be a valid information");

            RuleFor(c => c.Email)
               .NotEmpty().WithMessage("Please ensure you have entered the Email")
               .EmailAddress().WithMessage("Please ensure you have entered a valid Email")
               .MustAsync(BeUniqueEmail).WithMessage("Email already registered");

            RuleFor(x => x.IsActive).NotNull();
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var client = await _unitOfWork.MongoClientRepository.GetClientByEmailAsync(email);
            return client == null;
        }
    }
}
