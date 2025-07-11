using MongoDB.Driver;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class UrlShortnerDBContext
    {
        public IMongoCollection<ShortUrl> ShortUrls {  get; set; }

        public UrlShortnerDBContext(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            ShortUrls = database.GetCollection<ShortUrl>(configuration["DatabaseSettings:CollectionName"]);
        }
    }
}
