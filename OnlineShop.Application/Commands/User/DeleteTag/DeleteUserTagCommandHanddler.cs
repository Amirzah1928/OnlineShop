using MediatR;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.DeleteTag
{
    public class DeleteUserTagCommandHanddler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserTagCommand>
    {
        public async Task Handle(DeleteUserTagCommand request, CancellationToken cancellationToken)
        {
            var User = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
           ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.User)));

            User.RemoveTag(request.Title, request.Priority);

            unitOfWork.UserRepository.Update(User);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
