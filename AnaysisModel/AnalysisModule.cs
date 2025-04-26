namespace AnaysisModel
{
    public class AnalysisModule
    {
        public GrowthPrediction CalculateGrowthPotential(Strategy strategy, float cost, int headcount)
        {
            return new GrowthPrediction
            {
                Percentage = (cost * 0.1f + headcount * 0.2f) / 10
            };
        }
    }
}
