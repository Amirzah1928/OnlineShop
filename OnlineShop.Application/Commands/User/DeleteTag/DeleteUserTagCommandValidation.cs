
using FluentValidation;
using OnlineShop.DomainModel.Models;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.DeleteTag
{
    public class DeleteUserTagCommandValidation : AbstractValidator<DeleteUserTagCommand>
    {
        public DeleteUserTagCommandValidation()
        {
            RuleFor(x => x.Title)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Requierd, nameof(UserTag.Title)));

            RuleFor(x => x.Priority)
                .NotNull().NotEmpty()
                .WithMessage(string.Format(Messages.Requierd, nameof(UserTag.Priority)));
        }
    }
}
