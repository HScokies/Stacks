namespace Stacks_rework.Models.Output
{
    public class StackPreview
    {
        public StackPreview(string name, string thumb, long amount)
        {
            this.name = name;
            this.thumb = thumb;
            this.amount = amount;
        }
        public string name { get; set; } = null!;
        public string thumb { get; set; } = null!;
        public long amount { get; set; } = 0;
    }
}
