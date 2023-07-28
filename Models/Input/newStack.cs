namespace Stacks_rework.Models.Input
{
    public class newStack
    {
        public string uid { get; set; } = null!;
        public string token { get; set; } = null!;
        List<Card> cards { get; set; } = new List<Card>();
    }
}
