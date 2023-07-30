using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace Stacks_rework.Models
{
    public class BaseStack
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string token { get; set; } = null!;
        [Url]
        public string? thumbnail { get; set; } = null!;
        public string name { get; set; } = null!;
        public List<Card> cards { get; set; } = null!;
    }
}
