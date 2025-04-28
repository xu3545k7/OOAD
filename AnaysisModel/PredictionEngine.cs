using static alglib;
namespace AnaysisModel
{
    public class PredictionEngine
    {
        private readonly List<float> historicalROI = new List<float> { 5.0f, 6.5f, 7.2f, 8.0f, 9.3f };
        public ROIPrediction PredictROI(Strategy strategy, GrowthPrediction growth)
        {
            
            // 模擬新的 ROI 值並添加到歷史數據中
            float newROI = growth.GetPrediction() * 0.5f;
            historicalROI.Add(newROI);

            // 如果歷史數據不足兩個數值，返回當前的 ROI 值
            if (historicalROI.Count < 2)
            {
                return new ROIPrediction { Value = newROI };
            }

            // 準備線性回歸所需的數據
            int npoints = historicalROI.Count;
            double[,] xy = new double[npoints, 2];
            for (int i = 0; i < npoints; i++)
            {
                xy[i, 0] = i + 1; // 自變數（索引）
                xy[i, 1] = historicalROI[i]; // 因變數（ROI 值）
            }

            // 執行線性回歸
            linearmodel lm;
            lrreport rep;
            lrbuild(xy, npoints, 1, out lm, out rep);

            // 構建新的數據點，用於預測下一個值
            double[] newPoint = { npoints + 1.0 };
            double[] predictedValues = new double[1];
            alglib.xparams paramsObj = new alglib.xparams(0); // Provide a valid ulong argument

            // 使用 lrprocess 進行預測
            double predictedROI = lrprocess(lm, newPoint, paramsObj);

            return new ROIPrediction { Value = (float)predictedROI };
        }
        public void AddMockData()
        {
            // 增加模擬的歷史 ROI 數據
            historicalROI.AddRange(new float[] { 5.0f, 6.5f, 7.2f, 8.0f, 9.3f });
        }
    }
}
