namespace AnaysisModel
{
    public interface IAnalysisModule
    {
        GrowthPrediction CalculateGrowthPotential(Strategy strategy, List<float> historicalCost, List<int> historicalheadcount);
    }
}