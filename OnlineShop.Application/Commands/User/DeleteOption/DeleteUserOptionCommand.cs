using MediatR;

namespace OnlineShop.Application.Commands.User.DeleteOption
{
    public record DeleteUserOptionCommand(int Id, int OptionId) : IRequest;
}
