using Moq;
using Xunit;
using AnaysisModel;

namespace AnaysisModel.Tests
{
    public class ROIPredictionTests
    {
        [Fact]
        public void GetPrediction_ReturnsCorrectValue()
        {
            // Arrange
            var mockROIPrediction = new Mock<ROIPrediction>();
            mockROIPrediction.Setup(p => p.GetPrediction()).Returns(12.3f);

            // Act
            var result = mockROIPrediction.Object.GetPrediction();

            // Assert
            Assert.Equal(12.3f, result);
        }

        [Fact]
        public void HistoricalROI_SetAndGet_ReturnsExpectedArray()
        {
            // Arrange
            var roiPrediction = new ROIPrediction
            {
                historicalROI = new float[] { 5.0f, 6.5f, 7.2f }
            };

            // Act
            var result = roiPrediction.historicalROI;

            // Assert
            Assert.Equal(new float[] { 5.0f, 6.5f, 7.2f }, result);
        }

        [Fact]
        public void GethistoricalROI_ReturnsCorrectArray()
        {
            // Arrange
            var mockROIPrediction = new Mock<ROIPrediction>();
            mockROIPrediction.Setup(p => p.GethistoricalROI()).Returns(new float[] { 10.0f, 12.5f, 15.2f });

            // Act
            var result = mockROIPrediction.Object.GethistoricalROI();

            // Assert
            Assert.Equal(new float[] { 10.0f, 12.5f, 15.2f }, result);
        }

        [Fact]
        public void PredictROI_ShouldReturnNonZeroValue()
        {
            // Arrange
            var predictionEngine = new PredictionEngine();
            var strategy = new Strategy { Id = 1, Description = "Strategy 1" };
            var growthPrediction = new GrowthPrediction { Percentage = 15.0f };
            var historicalROI = new List<float> { 5.0f, 6.8f, 7.5f, 8.7f, 10.2f };

            // Act
            var result = predictionEngine.PredictROI(strategy, growthPrediction, historicalROI);

            // Assert
            Assert.Equal(11.3299999f, result.GetPrediction());
        }

        [Fact]
        public void PredictROI_ShouldReturnZeroValue()
        {
            // Arrange
            var predictionEngine = new PredictionEngine();
            var strategy = new Strategy { Id = 1, Description = "Strategy 1" };
            var growthPrediction = new GrowthPrediction { Percentage = 15.0f };
            var historicalROI = new List<float> { 5.0f };

            // Act
            var result = predictionEngine.PredictROI(strategy, growthPrediction, historicalROI);

            // Assert
            Assert.Equal(0.0f, result.GetPrediction());
        }
    }
}