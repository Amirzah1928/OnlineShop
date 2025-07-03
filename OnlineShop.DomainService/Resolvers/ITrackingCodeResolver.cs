using OnlineShop.DomainService.Proxies;

namespace OnlineShop.DomainService.Resolvers
{
    public interface ITrackingCodeResolver
    {
        public ITrackingCodeProxy Resolve(int count);
    }
}
