using MediatR;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetList
{
    public record GetUserListQueries(string Q,int PageSize, int PageNumber, OrderType OrderType) : IRequest<PaginationResult<UserViewModel>>;
}
