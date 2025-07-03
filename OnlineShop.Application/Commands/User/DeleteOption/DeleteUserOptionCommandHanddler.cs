using MediatR;
using OnlineShop.DomainService.Data;
using OnlineShop.DomainService.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.Application.Commands.User.DeleteOption
{
    public class DeleteUserOptionCommandHanddler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserOptionCommand>
    {
        public async Task Handle(DeleteUserOptionCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
           ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.User)));

            user.RemoveOption(request.Id);

            unitOfWork.UserRepository.Update(user);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
