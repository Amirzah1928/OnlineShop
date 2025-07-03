using MediatR;
using OnlineShop.Application.Events.User.Changes;
using OnlineShop.DomainService.Services;

namespace OnlineShop.Application.Commands.User.Update
{
    public class UpdateUserCommandHandler(IMediator mediator, IUserService service) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
           var user = await service.UpdateAsync(request.Id,request.FirstName, request.LastName, request.PhoneNumber, request.Coordinate, request.Type, cancellationToken);

            var bookChangeEvent = user.ToEvent();
            Task.Run(async () => mediator.Publish(bookChangeEvent));
        }
    }
}

