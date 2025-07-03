using MediatR;
using OnlineShop.DomainService.Services;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetById
{
    public class GetUserByIdQueriesHandler(IUserService service) : IRequestHandler<GetUserByIdQueries, UserViewModel>
    {
        public async Task<UserViewModel> Handle(GetUserByIdQueries request, CancellationToken cancellationToken)
        {
           return await service.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
