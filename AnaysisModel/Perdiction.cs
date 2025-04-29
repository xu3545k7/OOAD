namespace AnaysisModel
{
    public class Prediction
    {
        public float Value { get; set; }
        public float[] historical { get; set; }
        public float GetPrediction()
        {
            return Value;
        }

        public float[] Gethistorical()
        {
            return historical;
        }
    }
}
