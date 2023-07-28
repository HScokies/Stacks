namespace Stacks_rework.Models
{
    public class AdvertisementStack : BaseStack
    {
        public Organisation organisation { get; set; } = null!;
        protected bool isActive { get; set; } = false;
    }
}
