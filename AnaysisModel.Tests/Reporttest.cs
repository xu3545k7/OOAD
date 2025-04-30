using System.Collections.Generic;
using Xunit;

namespace AnaysisModel.Tests
{
    public class ReportTests
    {
        [Fact]
        public void AddUserSelectedStrategy_ShouldAddHistoricalDataToCorrectStrategy()
        {
            // Arrange
            var strategies = new List<Strategy>
            {
                new Strategy { Id = 1, Description = "Strategy 1" },
                new Strategy { Id = 2, Description = "Strategy 2" },
                new Strategy { Id = 3, Description = "Strategy 3" }
            };
            var report = new Report
            {
                Strategies = strategies,
                GrowthPredictions = new List<GrowthPrediction>(),
                ROIPredictions = new List<ROIPrediction>()
            };

            int strategyId = 2;
            float cost = 200000f;
            int headcount = 5;
            float roi = 10.5f;

            // Act
            report.AddUserSelectedStrategy(strategyId, cost, headcount, roi);

            // Assert
            var selectedStrategy = strategies.Find(s => s.Id == strategyId);
            Assert.NotNull(selectedStrategy);
            Assert.Contains(cost, selectedStrategy.historicalCost);
            Assert.Contains(headcount, selectedStrategy.historicalheadcount);
            Assert.Contains(roi, selectedStrategy.historicalROI);
        }

        [Fact]
        public void GenerateSummary_ShouldReturnCorrectSummary()
        {
            // Arrange
            var strategies = new List<Strategy>
            {
                new Strategy { Id = 1, Description = "Strategy 1" },
                new Strategy { Id = 2, Description = "Strategy 2" },
                new Strategy { Id = 3, Description = "Strategy 3" }
            };

            var growthPredictions = new List<GrowthPrediction>
            {
                new GrowthPrediction { Percentage = 15.0f },
                new GrowthPrediction { Percentage = 20.0f },
                new GrowthPrediction { Percentage = 25.0f }
            };

            var roiPredictions = new List<ROIPrediction>
            {
                new ROIPrediction { Value = 8.0f },
                new ROIPrediction { Value = 10.0f },
                new ROIPrediction { Value = 12.0f }
            };

            var report = new Report
            {
                Strategies = strategies,
                GrowthPredictions = growthPredictions,
                ROIPredictions = roiPredictions
            };

            // Act
            var summary = report.GenerateSummary();

            // Assert
            Assert.Contains("Strategy 1", summary);
            Assert.Contains("Growth Potential: 15%", summary);
            Assert.Contains("Predicted ROI: 8%", summary);

            Assert.Contains("Strategy 2", summary);
            Assert.Contains("Growth Potential: 20%", summary);
            Assert.Contains("Predicted ROI: 10%", summary);

            Assert.Contains("Strategy 3", summary);
            Assert.Contains("Growth Potential: 25%", summary);
            Assert.Contains("Predicted ROI: 12%", summary);
        }

        [Fact]
        public void GenerateSummary_ShouldHandleEmptyStrategies()
        {
            // Arrange
            var report = new Report
            {
                Strategies = new List<Strategy>(),
                GrowthPredictions = new List<GrowthPrediction>(),
                ROIPredictions = new List<ROIPrediction>()
            };

            // Act
            var summary = report.GenerateSummary();

            // Assert
            Assert.Equal("Analysis Summary:\n", summary);
        }
    }
}
