using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Endpoints
{
    public class EditoriginalUrlEndpointRequest
    {
        public string OriginalUrl { get; set; } = default!;
    }

    public class EditoriginalUrlEndpoint(UrlShortnerDBContext db, IMemoryCache memoryCache) : Endpoint<EditoriginalUrlEndpointRequest>
    {
        private readonly IMongoCollection<ShortUrl> _collection = db.ShortUrls;

        public override void Configure()
        {
            Put("/{code}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EditoriginalUrlEndpointRequest req, CancellationToken ct)
        {
            var code = Route<string>("code")!;

            var option = new UpdateOptions { BypassDocumentValidation = false };

            var result = await _collection.UpdateOneAsync(
            Builders<ShortUrl>.Filter.Eq(x => x.Code, code),
            Builders<ShortUrl>.Update.Set(x => x.OriginalUrl, req.OriginalUrl),
            option,
            ct
            );

            if (result.MatchedCount == 0)
            {
                await SendNotFoundAsync();
                return;
            }

            memoryCache.Remove(code);
        }
    }
}
