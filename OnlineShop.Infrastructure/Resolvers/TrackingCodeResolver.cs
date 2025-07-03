using Microsoft.Extensions.DependencyInjection;
using OnlineShop.DomainService.Proxies;
using OnlineShop.DomainService.Resolvers;
using OnlineShop.Infrastructure.Proxies;


namespace OnlineShop.Infrastructure.Resolvers
{
    public class TrackingCodeResolver(IServiceProvider serviceProvider) : ITrackingCodeResolver
    {
        public ITrackingCodeProxy Resolve(int count)
        {
            if (count < 10)
            {
                return serviceProvider.GetRequiredService<TrackingCodeProxy>();
            }
            else
            {
                return serviceProvider.GetRequiredService<LocalTrackingCodeProxy>();
            }
        }
    }
}
