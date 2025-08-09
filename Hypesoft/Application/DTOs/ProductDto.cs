using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Hypesoft.Application.DTOs
{
    public class ProductDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int StockQuantity { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DtCriacao { get; set; } = DateTime.UtcNow;
    }
}
