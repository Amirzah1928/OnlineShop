using Microsoft.Extensions.Caching.Memory;
using OnlineShop.APIs.Exceptions;
using OnlineShop.Resources.Messages;

namespace OnlineShop.API.Middlewares
{
    public class RateLimitMiddleware(RequestDelegate next, IMemoryCache memoryCache)
    {
        private readonly TimeSpan timeLimit = TimeSpan.FromMinutes(1);
        private readonly int countLimit = 100;

        public async Task Invoke(HttpContext context)
        {
            var key = context.Connection.RemoteIpAddress!.ToString();

            memoryCache.TryGetValue(key, out int requestCount);

            if (requestCount > countLimit)
            {
                throw new TooManyRequestException(Messages.Toomanyrequests);
            }
            else
            {
                requestCount++;
                memoryCache.Set(key, requestCount, timeLimit);

                await next(context);
            }
        }
    }
}
