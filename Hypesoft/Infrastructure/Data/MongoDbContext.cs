using Hypesoft.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Data
{
    public class MongoDbContext
    {

        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDb:ConnectionString"]);
            _database = client.GetDatabase(config["MongoDb:DatabaseName"]);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    }
}
