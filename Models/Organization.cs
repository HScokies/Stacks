using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class Organization
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public string? name { get; set; } = null!;
        public string? logo { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string token { get; set; } = null!;
    }
}
