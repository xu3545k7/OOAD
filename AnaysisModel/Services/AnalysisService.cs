using System.Collections.Generic;
using System;
using AnaysisModel.Interfaces;
namespace AnaysisModel.Model
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisModule _analysisModule;
        private readonly IPredictionEngine _predictionEngine;

        // 新增建構函式，接受依賴
        public AnalysisService(IAnalysisModule analysisModule, IPredictionEngine predictionEngine)
        {
            _analysisModule = analysisModule ?? throw new ArgumentNullException(nameof(analysisModule));
            _predictionEngine = predictionEngine ?? throw new ArgumentNullException(nameof(predictionEngine));
        }

        private enum AnalysisServiceType
        {
            IncreaseMarketingBudget = 1,
            OptimizeOperations = 2,
            WithAdditionalRequirement = 3
        }

        private AnalysisServiceType _currentServiceType = AnalysisServiceType.IncreaseMarketingBudget;


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



        public virtual Report AnalyzeStrategies(float cost, int headcount, float roi, String additional_requirement)
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

                // 將用戶輸入的數據添加到策略的歷史數據中
                List<float> historicalCost = strategy.GethistoricalCost();
                historicalCost.Add(cost);
                List<int> historicalheadcount = strategy.Gethistoricalheadcount();
                historicalheadcount.Add(headcount);
                List<float> historicalROI = strategy.GethistoricalROI();
                historicalROI.Add(roi);

                var growth = _analysisModule.CalculateGrowthPotential(strategy, historicalCost, strategy.historicalheadcount);
                growthPredictions.Add(growth);


                var predictedROI = _predictionEngine.PredictROI(strategy, growth, historicalROI);
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
