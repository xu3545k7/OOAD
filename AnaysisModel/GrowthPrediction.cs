using System;
using static alglib;
namespace AnaysisModel
{

    public class GrowthPrediction
    {
        public float Percentage { get; set; }

        public float GetPrediction()
        {
            return Percentage;
        }
    }
}
