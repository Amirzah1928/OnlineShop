using FastEndpoints;
using MongoDB.Driver;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Endpoints
{
    public class ListShortURLsEndpointResponse
    {
        public List<ShortUrl> ShortUrls { get; set; } = default!;
    }


    public class ListShortUrlsEndpoint(UrlShortnerDBContext db) : EndpointWithoutRequest<ListShortURLsEndpointResponse>
    {
        private readonly IMongoCollection<ShortUrl> _collection = db.ShortUrls;

        public override void Configure()
        {
            Get("/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _collection.Find(FilterDefinition<ShortUrl>.Empty).ToListAsync(ct);
            await SendAsync(new ListShortURLsEndpointResponse { ShortUrls = result }, StatusCodes.Status200OK, ct);
        }
    }
}
