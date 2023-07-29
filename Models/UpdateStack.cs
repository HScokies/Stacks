namespace Stacks_rework.Models
{
    public class UpdateStack
    {
        public string thumbnail { get; set; } = null!;
        public string name { get; set; } = null!;
        public List<Card> cards { get; set; } = null!;
    }
}
