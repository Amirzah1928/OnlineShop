using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Endpoints
{
    public class DeleteShortUrlEndpoint(UrlShortnerDBContext db, IMemoryCache memoryCache) : EndpointWithoutRequest
    {
        private readonly IMongoCollection<ShortUrl> _collection = db.ShortUrls;

        public override void Configure()
        {
            Delete("/{code}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var code = Route<string>("code")!;

            var result = await _collection.DeleteOneAsync(
                Builders<ShortUrl>.Filter.Eq(x => x.Code, code),
                ct
                );

            if (result.DeletedCount == 0)
            {
                await SendNotFoundAsync(ct);
                return;
            }
            memoryCache.Remove(code);
        }
    }
}
