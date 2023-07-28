using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class UserStack
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        
        [StringLength(9, ErrorMessage = "invalid user id")]
        public string uid { get; set; } = null!;
        [JsonIgnore]
        public string? token { get; set; } = null!;
        [JsonIgnore]
        public bool isPrivate { get; set; } = false;
        public string name { get; set; } = null!;
        public List<Card> cards { get; set; } = null!;
    }
}
