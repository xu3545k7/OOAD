using System.Collections.Generic;
using System;
namespace AnaysisModel
{
    public class AnalysisService
    {
        private readonly AnalysisModule _analysisModule = new AnalysisModule();
        private readonly PredictionEngine _predictionEngine = new PredictionEngine();

        // 暫時取代資料庫的部分
        private readonly List<float> historicalCost_Increase_Marketing_Budget = new List<float> { 170000, 175000, 172000, 179000, 180000 };
        private readonly List<int> historicalheadcount_Increase_Marketing_Budget = new List<int> { 2, 3, 3, 4, 4 };
        private readonly List<float> historicalROI_Increase_Marketing_Budget = new List<float> { 5.0f, 6.8f, 7.5f, 8.7f, 10.2f };
        private readonly List<float> historicalCost_Optimize_Operations = new List<float> { 170000, 175000, 172000, 179000, 180000 };
        private readonly List<int> historicalheadcount_Optimize_Operations = new List<int> { 2, 3, 3, 4, 4 };
        private readonly List<float> historicalROI_Optimize_Operations = new List<float> { 5.0f, 6.8f, 7.5f, 7.7f, 9.2f };
        private readonly List<float> historicalCost_Additional_Requirement = new List<float> { 170000, 175000, 172000, 179000, 180000 };
        private readonly List<int> historicalheadcount_Additional_Requirement = new List<int> { 2, 3, 3, 4, 4 };
        private readonly List<float> historicalROI_With_Additional_Requirement = new List<float> { 4.0f, 5.5f, 6.0f, 6.5f, 8.0f };



        public Report AnalyzeStrategies(float cost, int headcount, float roi, String additional_requirement)
        {
            var strategies = new List<Strategy>
            {
                new Strategy { Id = 1, Description = "Increase Marketing Budget" ,
                    historicalCost = historicalCost_Increase_Marketing_Budget ,
                    historicalheadcount = historicalheadcount_Increase_Marketing_Budget,
                    historicalROI = historicalROI_Increase_Marketing_Budget },
                new Strategy { Id = 2, Description = "Optimize Operations",
                    historicalCost = historicalCost_Optimize_Operations ,
                    historicalheadcount = historicalheadcount_Optimize_Operations,
                    historicalROI = historicalROI_Optimize_Operations },
                new Strategy { Id = 3, Description = "With Additional Requirement",
                    historicalCost = historicalCost_Additional_Requirement ,
                    historicalheadcount = historicalheadcount_Additional_Requirement,
                    historicalROI = historicalROI_With_Additional_Requirement }
            };

            var growthPredictions = new List<GrowthPrediction>();
            var roiPredictions = new List<ROIPrediction>();

            foreach (var strategy in strategies)
            {
                if (strategy.Description == "With Additional Requirement")
                {
                    // todo : 這裡需要根據額外需求來growth的預測
                }

                var growth = _analysisModule.CalculateGrowthPotential(strategy, strategy.GethistoricalCost(), strategy.Gethistoricalheadcount());
                growthPredictions.Add(growth);


                var predictedROI = _predictionEngine.PredictROI(strategy, growth, strategy.GethistoricalROI());
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
