using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class UserStack : BaseStack
    {       
        [StringLength(9, ErrorMessage = "invalid user id")]
        public string uid { get; set; } = null!;
        [JsonIgnore]
        public bool isPrivate { get; set; } = false;
    }
}
