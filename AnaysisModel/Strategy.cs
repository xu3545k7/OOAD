namespace AnaysisModel
{
    public class Strategy
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string GetDetails()
        {
            return $"Strategy {Id}: {Description}";
        }
    }
}
