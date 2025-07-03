using FluentValidation;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.FirstName)))

                .MaximumLength(50)
                .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.FirstName), 50));

            RuleFor(x => x.LastName)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.LastName)))

                .MaximumLength(50)
                .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.FirstName), 50));

            RuleFor(x => x.PhoneNumber)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.PhoneNumber)))

                .MaximumLength(11)
                .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.PhoneNumber), 11))

                .MinimumLength(11)
                .WithMessage(string.Format(Messages.MinValue, nameof(DomainModel.Models.User.PhoneNumber), 11))

                .Must(p => p.StartsWith("09"))
                .WithMessage(string.Format(Messages.InvalidPhoneNumber, nameof(DomainModel.Models.User.PhoneNumber)));

            RuleFor(x => x.Coordinate.Longitude)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.Coordinate.Longitude)));

            RuleFor(x => x.Coordinate.Latitude)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.Coordinate.Latitude)));

            RuleFor(x => x.Type)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.Type)));

        }
    }
}
