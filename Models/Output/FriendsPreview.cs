namespace Stacks_rework.Models.Output
{
    public class FriendsPreview
    {
        public FriendsPreview(string uid, long amount)
        {
            this.uid = uid;
            this.amount = amount;
        }
        public string uid { get; set; } = null!; // VK Web App Get User Info
        public long amount { get; set; } = 0;
    }
}
