using FastEndpoints;
using MongoDB.Driver;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Endpoints
{
    public class ShortnerUrlEndpointRequest
    {
        public string OriginalUrl { get; set; } = default!;
    }

    public class ShortnerUrlEndpointResponse
    {
        public string ShortUrl { get; set; } = default!;
    }


    public class ShortenerUrlEndpoint(UrlShortnerDBContext db, IHttpContextAccessor httpContextAccessor) : Endpoint<ShortnerUrlEndpointRequest, ShortnerUrlEndpointResponse>
    {
        private readonly IMongoCollection<ShortUrl> _collection = db.ShortUrls;

        public override void Configure()
        {
            Post("/Shorten");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ShortnerUrlEndpointRequest req, CancellationToken ct)
        {
            var code = CreateAsync(req.OriginalUrl, ct);

            var scheme = httpContextAccessor.HttpContext?.Request.Scheme;
            var host = httpContextAccessor.HttpContext?.Request.Host;
            var fullShortUrl = $"{scheme}://{host}/{code}";

            var result = new ShortnerUrlEndpointResponse { ShortUrl = fullShortUrl };
            await SendAsync(result, StatusCodes.Status200OK, ct);
        }




        private async Task<string> CreateAsync(string originalUrl, CancellationToken ct)
        {
            var code = GenerateCode();

            var shortUrl = new ShortUrl { Code = code, OriginalUrl = originalUrl };

            var option = new InsertOneOptions { BypassDocumentValidation = false };
            try
            {
                await _collection.InsertOneAsync(shortUrl, option, ct);
            }
            catch (MongoWriteException)
            {
                code = await CreateAsync(originalUrl, ct);
            }

            return code;
        }




        private static string GenerateCode(int lenght = 4)
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var randomChars = Enumerable.Repeat(characters, lenght).Select(c => c[Random.Shared.Next(characters.Length)]);

            return new string(randomChars.ToArray());
        }
    }
}
