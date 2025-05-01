using System;
using System.Drawing;
using AnaysisModel.Interfaces;

namespace AnaysisModel.Model
{
    public class AnalysisModule : IAnalysisModule
    {
        private readonly PredictionEngine predictionEngine = new PredictionEngine();
        public virtual GrowthPrediction CalculateGrowthPotential(Strategy strategy, List<float> historicalCost, List<int> historicalheadcount)
        {
            // 初始值
            float percentage = 0f;

            // 檢查策略是否為空
            if (strategy == null || string.IsNullOrEmpty(strategy.Description))
            {
                throw new ArgumentNullException(nameof(strategy), "Strategy cannot be null or empty.");
            }

            if (historicalCost == null || !historicalCost.Any())
            {
                throw new ArgumentException("Historical data cannot be null or empty.", nameof(historicalCost));
            }

            // 根據策略計算成長潛力百分比
            float cost = historicalCost[historicalCost.Count - 1];
            int headcount = historicalheadcount[historicalheadcount.Count - 1];



            if (strategy.Description == "Increase Marketing Budget")
            {
                // 假設增加市場預算，cost 的影響較大
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = CalculateImpact(cost, headcount, costImpact, headcountImpact, 0.001f, 0.5f);
            }
            else if (strategy.Description == "Optimize Operations")
            {
                // 假設優化運營，cost 的影響較小
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = CalculateImpact(cost, headcount, costImpact, headcountImpact, 0.0005f, 0.7f);
            }
            else if (strategy.Description == "With Additional Requirement")
            {
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = CalculateImpact(cost, headcount, costImpact, headcountImpact, 0.002f, 0.6f);
            }
            else
            {
                // 預設情況（可以根據需要進行調整）
                percentage = (cost * 0.1f + headcount * 0.1f) / 10;
            }

            return new GrowthPrediction
            {
                Percentage = percentage
            };
        }

        private float CalculateImpact(float cost, int headcount, float costImpact, int headcountImpact, float costWeight, float headcountWeight)
        {
            return (cost - costImpact) * costWeight + (headcountImpact - headcount) * headcountWeight;
        }


    }
}
