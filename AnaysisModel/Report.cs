using System.Collections.Generic;

namespace AnaysisModel
{
    public class Report
    {
        public List<Strategy> Strategies { get; set; }
        public List<GrowthPrediction> GrowthPredictions { get; set; }
        public List<ROIPrediction> ROIPredictions { get; set; }

        public string GenerateSummary()
        {
            var summary = "Analysis Summary:\n";
            for (int i = 0; i < Strategies.Count; i++)
            {
                summary += $"{Strategies[i].GetDetails()}\n" +
                           $"- Growth Potential: {GrowthPredictions[i].GetPrediction()}%\n" +
                           $"- Predicted ROI: {ROIPredictions[i].GetPrediction()}\n\n";
            }
            return summary;
        }
    }
}
