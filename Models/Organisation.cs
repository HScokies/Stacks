using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Stacks_rework.Models
{
    public class Organisation
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public string name { get; set; } = null!;
        public string logo { get; set; } = null!;
        public string token { get; set; } = null!;
    }
}
