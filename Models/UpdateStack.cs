namespace Stacks_rework.Models
{
    public class UpdateStack
    {
        public bool isPrivate { get; set; } = false;
        public string thumbnail { get; set; } = null!;
        public string name { get; set; } = null!;
        public List<Card> cards { get; set; } = null!;
    }
}
