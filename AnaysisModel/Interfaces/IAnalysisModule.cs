using AnaysisModel.Model;
namespace AnaysisModel.Interfaces
{
    public interface IAnalysisModule
    {
        GrowthPrediction CalculateGrowthPotential(Strategy strategy, List<float> historicalCost, List<int> historicalheadcount);
    }
}