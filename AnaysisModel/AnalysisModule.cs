using System;
using System.Drawing;
using Groq;
using static alglib;
namespace AnaysisModel
{
    public class AnalysisModule
    {
        private readonly PredictionEngine predictionEngine = new PredictionEngine();
        public GrowthPrediction CalculateGrowthPotential(Strategy strategy, List<float> historicalCost, List<int> historicalheadcount)
        {
            // 初始值
            float percentage = 0f;

            // 根據策略計算成長潛力百分比
            float cost = historicalCost[historicalCost.Count - 1];
            float headcount = historicalheadcount[historicalheadcount.Count - 1];



            if (strategy.Description == "Increase Marketing Budget")
            {
                // 假設增加市場預算，cost 的影響較大
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = (cost - costImpact) * 0.001f + (headcountImpact - headcount) * 0.5f;
            }
            else if (strategy.Description == "Optimize Operations")
            {
                // 假設優化運營，cost 的影響較小
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = (cost - costImpact) * 0.0005f + (headcountImpact - headcount) * 0.7f;
            }
            else if (strategy.Description == "With Additional Requirement")
            {
                float costImpact = predictionEngine.GrowthModel(historicalCost);
                int headcountImpact = predictionEngine.GrowthIntegerModel(historicalheadcount);
                percentage = (cost - costImpact) * 0.002f + (headcountImpact - headcount) * 0.6f;
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


    }
}
