using MediatR;

namespace OnlineShop.Application.Commands.User.ToggleActivation
{
    public record ToggleActivationUserCommand(int Id, CancellationToken Cancellation) : IRequest;
}
