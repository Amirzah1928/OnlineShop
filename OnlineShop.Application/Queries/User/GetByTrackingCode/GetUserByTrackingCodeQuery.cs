using MediatR;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetByTrackingCode
{
    public record GetUserByTrackingCodeQuery(string Code, string Prefix): IRequest<UserViewModel> 
    {
    }
}
