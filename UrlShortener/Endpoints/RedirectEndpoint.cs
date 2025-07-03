using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Endpoints
{
    public class RedirectEndpoint(UrlShortnerDBContext db, IMemoryCache memoryCache) : EndpointWithoutRequest
    {
        private readonly IMongoCollection<ShortUrl> _collection  = db.ShortUrls;


        public override void Configure()
        {
            Get("/{code}");
            AllowAnonymous();
        }


        public override async Task HandleAsync(CancellationToken ct)
        {
            var code = Route<string>("code")!;

            var shortUrl = memoryCache.Get<ShortUrl>(code);

            if (shortUrl == null)
            {
                shortUrl = await _collection.Find(x => x.Code == code).FirstOrDefaultAsync(ct);

                if (shortUrl == null)
                {
                    await SendNotFoundAsync();
                    return;
                }

                memoryCache.Set(code, shortUrl,TimeSpan.FromDays(1));
            }

            await SendRedirectAsync(shortUrl.OriginalUrl,true);
        }
    }
}
