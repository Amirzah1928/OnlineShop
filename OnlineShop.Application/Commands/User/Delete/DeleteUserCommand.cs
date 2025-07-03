using MediatR;

namespace OnlineShop.Application.Commands.User.Delete
{
    public record DeleteUserCommand(int Id, CancellationToken Cancellation) : IRequest;
}
