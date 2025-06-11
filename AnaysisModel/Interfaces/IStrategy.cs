namespace AnaysisModel.Interfaces
{
    public interface IStrategy
    {
        void AddHistoricalData(float cost, int headcount, float roi);
        string GetDetails();

        List<float> GethistoricalCost();
        List<int> Gethistoricalheadcount();

        List<float> GethistoricalROI();
    }
}