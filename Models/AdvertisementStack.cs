namespace Stacks_rework.Models
{
    public class AdvertisementStack : BaseStack
    {
        public Organisation organisation { get; set; } = null!;
        public bool isActive { get; set; } = false;
    }
}
