

using OnlineShop.DomainService.Proxies;

namespace OnlineShop.DomainService.Failovers
{
    public class FallbackTrackingCodeProxy(IEnumerable<ITrackingCodeProxy> trackingCodeProxies) : ITrackingCodeProxy
    {
        public int Priority => throw new NotImplementedException();

        public async Task<List<string>> Get(int count, CancellationToken cancellationToken)
        {
            foreach (var trackingCodeProxy in trackingCodeProxies.OrderBy(x => x.Priority))
            {
                try
                {
                    return await trackingCodeProxy.Get(count, cancellationToken);
                }
                catch
                {
                    // Log
                }
            }

            throw new Exception("TrackingCode services not available");
        }
    }
}
