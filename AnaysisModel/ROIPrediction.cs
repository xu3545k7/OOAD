namespace AnaysisModel
{
    public class ROIPrediction
    {
        public float Value { get; set; }
        public float[] historicalROI { get; set; }
        public float GetPrediction()
        {
            return Value;
        }

        public float[] GethistoricalROI()
        {
            return historicalROI;
        }
    }
}
