using MediatR;
using OnlineShop.Application.Commands.User.Delete;
using OnlineShop.Application.Events.User.Changes;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Services;

namespace OnlineShop.Commands.User.Delete
{
    public class DeleteUserCommandHandler(IMediator mediator, IUserService service) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
           var user = await service.DeleteAsync(request.Id,cancellationToken);

            var bookChangeEvent = user.ToEvent();
            Task.Run(async () => mediator.Publish(bookChangeEvent));
        }
    }
}