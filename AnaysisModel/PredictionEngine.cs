namespace AnaysisModel
{
    public class PredictionEngine
    {
        public ROIPrediction PredictROI(Strategy strategy, GrowthPrediction growth)
        {
            return new ROIPrediction
            {
                Value = growth.GetPrediction() * 0.5f
            };
        }
    }
}
