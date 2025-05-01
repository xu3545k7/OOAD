using System;

namespace AnaysisModel.Model
{

    public class GrowthPrediction
    {

        public virtual float cost { get; set; }
        public virtual int headcount { get; set; }
        public virtual float Percentage { get; set; }
        public virtual float GetPrediction()
        {
            return Percentage;
        }


    }
}
