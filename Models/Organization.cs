using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class Organization
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? name { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? logo { get; set; } = null!;
        public string token { get; set; } = null!;
    }
}
