using MediatR;
using OnlineShop.DomainService.Features;
using OnlineShop.DomainService.Services;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetList
{
    public class GetUserListQueriesHandler(IUserService service) : IRequestHandler<GetUserListQueries, PaginationResult<UserViewModel>>
    {
        public async Task<PaginationResult<UserViewModel>> Handle(GetUserListQueries request, CancellationToken cancellationToken)
        {
            return await service.GetListAsync(request.Q,request.PageSize,request.PageNumber,request.OrderType,cancellationToken);
        }
    }
}
