using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GigaHouse.Core.Common.Logging
{
    public class RequestLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public required string Id { get; set; }

        public required BsonDocument RequestBody { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
