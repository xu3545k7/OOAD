using System.Collections.Generic;
using AnaysisModel.Interfaces;

namespace AnaysisModel.Model
{
    public class Report : IReport
    {
        public List<Strategy> Strategies { get; set; }
        public List<GrowthPrediction> GrowthPredictions { get; set; }
        public List<ROIPrediction> ROIPredictions { get; set; }
        public String additional_requirement { get; set; }

        public void AddUserSelectedStrategy(int strategyId, float cost, int headcount, float roi)
        {
            var selectedStrategy = Strategies.Find(s => s.Id == strategyId);
            if (selectedStrategy != null)
            {
                selectedStrategy.AddHistoricalData(cost, headcount, roi);
            }
        }

        public string GenerateSummary()
        {

            var summary = "Analysis Summary:\n";

            if (Strategies == null || Strategies.Count == 0)
            {
                return "No strategies available.";
            }

            for (int i = 0; i < Strategies.Count; i++)
            {
                summary += $"{Strategies[i].GetDetails()}\n" +
                            $"- Cost of Strategy: {GrowthPredictions[i].cost}\n" +
                            $"- Headcount of Strategy: {GrowthPredictions[i].headcount}\n" +
                            $"- Growth Potential: {GrowthPredictions[i].GetPrediction()}%\n" +
                            $"- Predicted ROI: {ROIPredictions[i].GetPrediction()}%\n\n";
            }
            return summary;
        }
    }
}
