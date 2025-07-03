using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        [BsonId]
        public string Code { get; set; } = default!;
        public string OriginalUrl { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
