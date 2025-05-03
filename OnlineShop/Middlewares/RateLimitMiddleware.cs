using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Exceptions;

namespace OnlineShop.Middlewares
{
    public class RateLimitMiddleware(RequestDelegate next, IMemoryCache memoryCache)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();

            if (ip == null)
            {
                await next(context);
                return;
            }

            var key = $"RateLimit_{ip}";

            int count = memoryCache.GetOrCreate<int>(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return 0;
            });

            if (count >= 10)
             throw new TooManyRequestException("Too many request");
            


            memoryCache.Set(key, count + 1, DateTime.Now.AddMinutes(1));

            await next(context);
        }
    }
}
