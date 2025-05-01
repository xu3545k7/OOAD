namespace AnaysisModel.Interfaces
{
    public interface IReport
    {
        void AddUserSelectedStrategy(int strategyId, float cost, int headcount, float roi);
        string GenerateSummary();
    }
}
