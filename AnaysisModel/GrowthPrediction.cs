using System;

namespace AnaysisModel
{

    public class GrowthPrediction
    {

        public float cost { get; set; }
        public int headcount { get; set; }
        public float Percentage { get; set; }
        public float GetPrediction()
        {
            return Percentage;
        }


    }
}
