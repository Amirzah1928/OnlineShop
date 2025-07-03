
using MediatR;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.CreateTag
{
    public class CreateUserTagCommandHanddler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserTagCommand>
    {
        public async Task Handle(CreateUserTagCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.User)));

            user.AddTag(request.Title, request.Priority);

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
