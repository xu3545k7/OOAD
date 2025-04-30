using Moq;
using Xunit;
using AnaysisModel;
using System.Collections.Generic;

namespace AnaysisModel.Tests
{
    public class AnalysisServiceTests
    {
        [Fact]
        public void AnalyzeStrategies_ShouldGenerateCorrectNumberOfStrategies()
        {
            // Arrange
            var mockAnalysisModule = new Mock<IAnalysisModule>();
            var mockPredictionEngine = new Mock<IPredictionEngine>();
            var service = new AnalysisService(mockAnalysisModule.Object, mockPredictionEngine.Object);

            float cost = 180000;
            int headcount = 5;
            float roi = 9.5f;
            string additionalRequirement = "Custom Requirement";

            mockAnalysisModule
                .Setup(m => m.CalculateGrowthPotential(It.IsAny<Strategy>(), It.IsAny<List<float>>(), It.IsAny<List<int>>()))
                .Returns(new GrowthPrediction { Percentage = 15.5f });

            mockPredictionEngine
                .Setup(p => p.PredictROI(It.IsAny<Strategy>(), It.IsAny<GrowthPrediction>(), It.IsAny<List<float>>()))
                .Returns(new ROIPrediction { Value = 10.5f });

            // Act
            var report = service.AnalyzeStrategies(cost, headcount, roi, additionalRequirement);

            // Assert
            Assert.NotNull(report);
            Assert.Equal(3, report.Strategies.Count);
            Assert.Equal(3, report.GrowthPredictions.Count);
            Assert.Equal(3, report.ROIPredictions.Count);
        }

        [Fact]
        public void AnalyzeStrategies_ShouldUpdateHistoricalData()
        {
            // Arrange
            var mockAnalysisModule = new Mock<IAnalysisModule>();
            var mockPredictionEngine = new Mock<IPredictionEngine>();
            var service = new AnalysisService(mockAnalysisModule.Object, mockPredictionEngine.Object);

            float cost = 180000;
            int headcount = 5;
            float roi = 9.5f;

            mockAnalysisModule
                .Setup(m => m.CalculateGrowthPotential(It.IsAny<Strategy>(), It.IsAny<List<float>>(), It.IsAny<List<int>>()))
                .Returns(new GrowthPrediction { Percentage = 12.5f });

            mockPredictionEngine
                .Setup(p => p.PredictROI(It.IsAny<Strategy>(), It.IsAny<GrowthPrediction>(), It.IsAny<List<float>>()))
                .Returns(new ROIPrediction { Value = 8.5f });

            // Act
            var report = service.AnalyzeStrategies(cost, headcount, roi, string.Empty);

            // Assert
            foreach (var strategy in report.Strategies)
            {
                Assert.Contains(cost, strategy.historicalCost);
                Assert.Contains(headcount, strategy.historicalheadcount);
                Assert.Contains(roi, strategy.historicalROI);
            }
        }

        [Fact]
        public void AnalyzeStrategies_WithCustomRequirement_ShouldHandleCorrectly()
        {
            // Arrange
            var mockAnalysisModule = new Mock<IAnalysisModule>();
            var mockPredictionEngine = new Mock<IPredictionEngine>();
            var service = new AnalysisService(mockAnalysisModule.Object, mockPredictionEngine.Object);

            float cost = 200000;
            int headcount = 7;
            float roi = 12.0f;
            string additionalRequirement = "Special Case";

            mockAnalysisModule
                .Setup(m => m.CalculateGrowthPotential(It.IsAny<Strategy>(), It.IsAny<List<float>>(), It.IsAny<List<int>>()))
                .Returns(new GrowthPrediction { Percentage = 20.0f });

            mockPredictionEngine
                .Setup(p => p.PredictROI(It.IsAny<Strategy>(), It.IsAny<GrowthPrediction>(), It.IsAny<List<float>>()))
                .Returns(new ROIPrediction { Value = 15.0f });

            // Act
            var report = service.AnalyzeStrategies(cost, headcount, roi, additionalRequirement);

            // Assert
            Assert.NotNull(report);
            Assert.Contains(report.Strategies, s => s.Description == "With Additional Requirement");
        }

    }
}
