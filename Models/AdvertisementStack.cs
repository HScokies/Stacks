namespace Stacks_rework.Models
{
    public class AdvertisementStack : BaseStack
    {
        public Organization? organization { get; set; } = null!;
        public bool isActive { get; set; } = false;
    }
}
