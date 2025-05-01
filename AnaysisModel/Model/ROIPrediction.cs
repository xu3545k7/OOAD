namespace AnaysisModel.Model
{
    public class ROIPrediction
    {
        public virtual float Value { get; set; }
        public virtual float[] historicalROI { get; set; }
        public virtual float GetPrediction()
        {
            return Value;
        }

        public virtual float[] GethistoricalROI()
        {
            return historicalROI;
        }
    }
}
