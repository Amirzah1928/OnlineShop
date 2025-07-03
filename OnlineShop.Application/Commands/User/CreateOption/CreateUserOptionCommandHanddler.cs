
using MediatR;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.CreateOption
{
    public class CreateUserOptionCommandHanddler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserOptionCommand>
    {
        public async Task Handle(CreateUserOptionCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.User)));

            user.AddOption(request.Description);

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
