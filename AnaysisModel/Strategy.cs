namespace AnaysisModel
{
    public class Strategy
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public List<float> historicalCost { get; set; } = new List<float>();

        public List<int> historicalheadcount { get; set; } = new List<int>();

        public List<float> historicalROI { get; set; } = new List<float>();

        public void AddHistoricalData(float cost, int headcount, float roi)
        {
            historicalCost.Add(cost);
            historicalheadcount.Add(headcount);
            historicalROI.Add(roi);
        }

        public string GetDetails()
        {
            return $"Strategy {Id}: {Description}";
        }

        public List<float> GethistoricalCost()
        {
            return historicalCost;
        }

        public List<int> Gethistoricalheadcount()
        {
            return historicalheadcount;
        }

        public List<float> GethistoricalROI()
        {
            return historicalROI;
        }
    }
}
