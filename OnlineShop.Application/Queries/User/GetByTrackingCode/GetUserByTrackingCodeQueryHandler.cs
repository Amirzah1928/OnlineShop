using MediatR;
using OnlineShop.DomainService.Services;
using OnlineShop.DomainService.ViewModels;

namespace OnlineShop.Application.Queries.User.GetByTrackingCode
{
    public class GetUserByTrackingCodeQueryHandler(IUserService service) : IRequestHandler<GetUserByTrackingCodeQuery, UserViewModel>
    {
        public async Task<UserViewModel> Handle(GetUserByTrackingCodeQuery request, CancellationToken cancellationToken)
        {
            return await service.GetTrackingCodeAsync(request.Code, request.Prefix, cancellationToken);
        }
    }
}
