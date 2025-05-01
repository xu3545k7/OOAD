using AnaysisModel.Model;
namespace AnaysisModel.Interfaces
{
    public interface IPredictionEngine
    {
        ROIPrediction PredictROI(Strategy strategy, GrowthPrediction growthPrediction, List<float> historicalROIs);
        public int GrowthIntegerModel(List<int> Historicaldata);
        public float GrowthModel(List<float> Historicaldata);

    }
}