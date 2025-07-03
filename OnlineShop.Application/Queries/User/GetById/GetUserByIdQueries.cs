using MediatR;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetById
{
    public record GetUserByIdQueries(int Id) : IRequest<UserViewModel>
    {

    }
}
