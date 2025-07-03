using OnlineShop.DomainService.Services;
using MediatR;
using OnlineShop.Application.Events.User.Changes;

namespace OnlineShop.Application.Commands.User.Create
{
    public class CreateUserCommandHandler(IMediator mediator, IUserService service) : IRequestHandler<CreateUserCommand>
    {
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
           var user = await service.CreateAsync(request.FirstName, request.LastName, request.PhoneNumber, request.Coordinate, request.Type, cancellationToken);

            var bookChangeEvent = user.ToEvent();
            Task.Run(async () => mediator.Publish(bookChangeEvent));
        }
    }
}
