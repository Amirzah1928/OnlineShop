using FluentValidation;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(5).WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.Id), 5));

            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.FirstName)))

                .MaximumLength(50)
                .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.FirstName), 50));

            RuleFor(x => x.LastName)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.LastName)))
                
                .MaximumLength(50)
                .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.User.LastName), 50));

            RuleFor(x => x.PhoneNumber)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(DomainModel.Models.User.PhoneNumber)))
                
                .Length(11, 11)
                .WithMessage(string.Format(Messages.InvalidPhoneNumber, nameof(DomainModel.Models.User.PhoneNumber)))
                
                .Must(p => p.StartsWith("09"))
                .WithMessage(string.Format(Messages.InvalidPhoneNumber, nameof(DomainModel.Models.User.PhoneNumber)));


            //DependentRules قوانین زنجیره ای
            //Custom قوانین سفارشی
        }
    }
}
