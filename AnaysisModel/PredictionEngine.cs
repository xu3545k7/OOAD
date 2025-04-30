using static alglib;

namespace AnaysisModel
{
    public class PredictionEngine : IPredictionEngine
    {
        public float GrowthModel(List<float> Historicaldata)
        {
            // 如果歷史數據不足兩個數值，返回當前的 ROI 值
            if (Historicaldata.Count < 2)
            {
                return 0.0f;
            }

            double predicted = linearFunction(Historicaldata);

            return (float)predicted;
        }
        public int GrowthIntegerModel(List<int> Historicaldata)
        {
            // 如果歷史數據不足兩個數值，返回當前的 ROI 值
            if (Historicaldata.Count < 2)
            {
                return 0;
            }

            // 準備線性回歸所需的數據
            int npoints = Historicaldata.Count;
            double[,] xy = new double[npoints, 2];
            for (int i = 0; i < npoints; i++)
            {
                xy[i, 0] = i + 1; // 自變數（索引）
                xy[i, 1] = Historicaldata[i]; // 因變數（ROI 值）
            }

            // 執行線性回歸
            linearmodel lm;
            lrreport rep;
            int info;
            lrbuild(xy, npoints, 1, out info, out lm, out rep);

            if (info != 1)
            {
                throw new Exception("Linear regression failed.");
            }

            // 構建新的數據點，用於預測下一個值
            double[] newPoint = { npoints + 1.0 };
            double[] predictedValues = new double[1];
            alglib.xparams paramsObj = new alglib.xparams(0); // Provide a valid ulong argument

            // 使用 lrprocess 進行預測
            double predicted = lrprocess(lm, newPoint, paramsObj);

            return (int)predicted;
        }

        public ROIPrediction PredictROI(Strategy strategy, GrowthPrediction growth, List<float> historicalROI)
        {

            // 如果歷史數據不足兩個數值，返回當前的 ROI 值
            if (historicalROI.Count < 2)
            {
                return new ROIPrediction { Value = 0.0f };
            }

            // 進行預測
            double predictedROI = linearFunction(historicalROI);

            return new ROIPrediction { Value = (float)predictedROI };
        }

        private float linearFunction(List<float> historicaldata)
        {

            // 如果歷史數據不足兩個數值，返回當前的 ROI 值
            if (historicaldata == null || historicaldata.Count < 2)
            {
                throw new Exception("Linear regression failed.");
            }

            try
            {
                // 準備線性回歸所需的數據
                int npoints = historicaldata.Count;
                double[,] xy = new double[npoints, 2];

                for (int i = 0; i < npoints; i++)
                {
                    xy[i, 0] = i + 1; // 索引作為自變數
                    xy[i, 1] = historicaldata[i]; // ROI 值作為因變數
                }

                linearmodel lm;
                lrreport rep;
                int info;
                lrbuild(xy, npoints, 1, out info, out lm, out rep);

                if (info != 1)
                {
                    throw new Exception("Linear regression failed.");
                }

                // 構建新的數據點，用於預測下一個值
                double[] newPoint = { npoints + 1.0 };
                double[] predictedValues = new double[1];
                alglib.xparams paramsObj = new alglib.xparams(0); // Provide a valid ulong argument

                // 使用 lrprocess 進行預測
                double predicted = lrprocess(lm, newPoint, paramsObj);
                return (float)predicted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Linear regression failed.，data: {string.Join(", ", historicaldata)}");
                throw new Exception("Linear regression failed.", ex);
            }
        }

        public void AddMockData(List<float> historicalROI)
        {
            // 增加模擬的歷史 ROI 數據
            historicalROI.AddRange(new float[] { 5.0f, 6.5f, 7.2f, 8.0f, 9.3f });
        }
        public void ClearMockData(List<float> historicalROI)
        {

            historicalROI.Clear();
        }
    }
}

