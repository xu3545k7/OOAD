using AnaysisModel.Model;
namespace AnaysisModel.Interfaces
{
    public interface IAnalysisService
    {
        Report AnalyzeStrategies(float cost, int headcount, float roi, string additional_requirement);
    }
}