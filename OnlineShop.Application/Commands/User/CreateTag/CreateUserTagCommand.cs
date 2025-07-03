using MediatR;

namespace OnlineShop.Application.Commands.User.CreateTag
{
    public record CreateUserTagCommand(int Id, string Title, int Priority) : IRequest;
}
