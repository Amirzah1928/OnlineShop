using MediatR;
using OnlineShop.Application.Events.User.Changes;
using OnlineShop.DomainModel.Models;
using OnlineShop.DomainService.Services;

namespace OnlineShop.Application.Commands.User.ToggleActivation
{
    public class ToggleActivationUserCommandHandler(IMediator mediator, IUserService service) : IRequestHandler<ToggleActivationUserCommand>
    {
        public async Task Handle(ToggleActivationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await service.ToggleActivationAsync(request.Id, cancellationToken);

            var bookChangeEvent = user.ToEvent();
            Task.Run(async () => mediator.Publish(bookChangeEvent));
        }
    }
}
