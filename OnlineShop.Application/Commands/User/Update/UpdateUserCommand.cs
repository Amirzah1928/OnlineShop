using MediatR;
using OnlineShop.DomainModel.Enums;
using OnlineShop.DomainModel.Models;

namespace OnlineShop.Application.Commands.User.Update
{
    public record UpdateUserCommand(int Id,string FirstName, string LastName, string PhoneNumber, Coordinate Coordinate, UserType Type) : IRequest;
}
