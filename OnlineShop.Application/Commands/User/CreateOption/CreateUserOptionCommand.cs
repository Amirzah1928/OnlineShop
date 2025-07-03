using MediatR;

namespace OnlineShop.Application.Commands.User.CreateOption
{
    public record CreateUserOptionCommand(int Id, string Description) : IRequest;
}
