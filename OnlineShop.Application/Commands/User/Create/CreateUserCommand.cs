using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;
using MediatR;

namespace OnlineShop.Application.Commands.User.Create
{
    public record CreateUserCommand(string FirstName, string LastName, string PhoneNumber, Coordinate Coordinate, UserType Type) : IRequest;
}
