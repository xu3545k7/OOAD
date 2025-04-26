using System.Collections.Generic;

namespace AnaysisModel
{
    public class AnalysisService
    {
        private readonly AnalysisModule _analysisModule = new AnalysisModule();
        private readonly PredictionEngine _predictionEngine = new PredictionEngine();

        public Report AnalyzeStrategies(float cost, int headcount, float roi)
        {
            var strategies = new List<Strategy>
            {
                new Strategy { Id = 1, Description = "Increase Marketing Budget" },
                new Strategy { Id = 2, Description = "Optimize Operations" }
            };

            var growthPredictions = new List<GrowthPrediction>();
            var roiPredictions = new List<ROIPrediction>();

            foreach (var strategy in strategies)
            {
                var growth = _analysisModule.CalculateGrowthPotential(strategy, cost, headcount);
                growthPredictions.Add(growth);

                var predictedROI = _predictionEngine.PredictROI(strategy, growth);
                roiPredictions.Add(predictedROI);
            }

            return new Report
            {
                Strategies = strategies,
                GrowthPredictions = growthPredictions,
                ROIPredictions = roiPredictions
            };
        }
    }
}
