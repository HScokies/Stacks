using Amazon.Auth.AccessControlPolicy;
using System.Text.Json.Serialization;

namespace Stacks_rework.Models
{
    public class AdvertisementStack : BaseStack
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Organization? organization { get; set; } = null!;
        public bool isActive { get; set; } = false;
    }
}
