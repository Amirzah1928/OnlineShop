using MediatR;

namespace OnlineShop.Application.Commands.User.DeleteTag
{
    public record DeleteUserTagCommand(int Id, string Title, int Priority) : IRequest;
}
