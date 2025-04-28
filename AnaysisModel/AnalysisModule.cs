using AnaysisModel;

namespace AnaysisModel
{
    public class AnalysisModule
    {
        public GrowthPrediction CalculateGrowthPotential(Strategy strategy, float cost, int headcount)
        {
            float percentage;
            ROIPrediction roiPrediction = new ROIPrediction();
            float[] historicalROI = roiPrediction.GethistoricalROI();
            // 根據策略計算成長潛力百分比
            
            if (historicalROI == null || historicalROI.Length == 0)
            {
                // 如果 historicalROI 為 null 或空陣列，則設置一個默認值
                historicalROI = new float[] { 0f }; // 假設 ROI 為 0%
            }
            if (strategy.Description == "Increase Marketing Budget")
            {
                // 假設增加市場預算，cost 的影響較大
                float costImpact  = cost * (1 + historicalROI[0] / 100);
                float headcountImpact = headcount * (1 + historicalROI[0] / 100);
                percentage = (costImpact - cost) + (headcountImpact - headcount) * 0.1f;
            }
            else if (strategy.Description == "Optimize Operations")
            {
                // 假設優化運營，cost 的影響較小
                percentage = headcount * 0.1f / 100;
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
