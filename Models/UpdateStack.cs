using Amazon.Auth.AccessControlPolicy;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class UpdateStack
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? isPrivate { get; set; } = false;
        public string? thumbnail { get; set; } = null!;
        public string name { get; set; } = null!;
        public List<Card> cards { get; set; } = null!;
    }
}
