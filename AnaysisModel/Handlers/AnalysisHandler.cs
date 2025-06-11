using AnaysisModel.Interfaces;
using AnaysisModel.Model;
using System;
using System.Collections.Generic;

namespace AnaysisModel.Handlers
{
    public class AnalysisHandler
    {
        private readonly IAnalysisModule _analysisModule;
        private readonly IAnalysisService _analysisService;
        private readonly IPredictionEngine _predictionEngine;
        private readonly IReport _report;
        private readonly IStrategy _strategy;

        public AnalysisHandler(IAnalysisModule analysisModule, IAnalysisService analysisService,
                               IPredictionEngine predictionEngine, IReport report, IStrategy strategy)
        {
            _analysisModule = analysisModule;
            _analysisService = analysisService;
            _predictionEngine = predictionEngine;
            _report = report;
            _strategy = strategy;
        }

        public void ProcessStrategy(int strategyId, float cost, int headcount, float roi, string additionalRequirement)
        {
            try
            {
                // Step 1: Add historical data to strategy
                _strategy.AddHistoricalData(cost, headcount, roi);

                // Step 2: Get historical data
                List<float> historicalCosts = _strategy.GethistoricalCost();
                List<int> historicalHeadcounts = _strategy.Gethistoricalheadcount();
                List<float> historicalROIs = _strategy.GethistoricalROI();

                // Step 3: Calculate growth potential
                GrowthPrediction growthPrediction = _analysisModule.CalculateGrowthPotential(
                    new Strategy(strategyId, cost, headcount, roi), historicalCosts, historicalHeadcounts);

                // Step 4: Predict ROI
                ROIPrediction roiPrediction = _predictionEngine.PredictROI(
                    new Strategy(strategyId, cost, headcount, roi), growthPrediction, historicalROIs);

                // Step 5: Generate detailed analysis
                Report detailedReport = _analysisService.AnalyzeStrategies(cost, headcount, roi, additionalRequirement);

                // Step 6: Add strategy details to report
                _report.AddUserSelectedStrategy(strategyId, cost, headcount, roi);

                // Step 7: Generate and display summary
                string summary = _report.GenerateSummary();

                Console.WriteLine("--- Strategy Analysis Summary ---");
                Console.WriteLine(summary);
                Console.WriteLine("--------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing strategy: {ex.Message}");
            }
        }
    }
}
