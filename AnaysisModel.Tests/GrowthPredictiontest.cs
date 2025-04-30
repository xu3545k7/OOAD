using Moq;
using Xunit;
using AnaysisModel;

namespace AnaysisModel.Tests
{
    public class GrowthPredictionTests
    {
        [Fact]
        public void GetPrediction_ReturnsCorrectPercentage()
        {
            // Arrange
            var mockGrowthPrediction = new Mock<GrowthPrediction>();
            mockGrowthPrediction.Setup(p => p.GetPrediction()).Returns(80.5f);

            // Act
            var result = mockGrowthPrediction.Object.GetPrediction();

            // Assert
            Assert.Equal(80.5f, result);
        }

        [Fact]
        public void Cost_SetAndGet_ReturnsExpectedValue()
        {
            // Arrange
            var growthPrediction = new GrowthPrediction();
            growthPrediction.cost = 1000f;

            // Act
            var result = growthPrediction.cost;

            // Assert
            Assert.Equal(1000f, result);
        }

        [Fact]
        public void Headcount_SetAndGet_ReturnsExpectedValue()
        {
            // Arrange
            var growthPrediction = new GrowthPrediction();
            growthPrediction.headcount = 50;

            // Act
            var result = growthPrediction.headcount;

            // Assert
            Assert.Equal(50, result);
        }

        [Fact]
        public void PredictGrowth_Value()
        {
            // Arrange
            var predictionEngine = new PredictionEngine();
            var historicalData = new List<float> { 170000, 175000, 172000, 179000, 180000 };


            // Act
            var result = predictionEngine.GrowthModel(historicalData);

            // Assert
            Assert.Equal(182400.0f, result);
        }

        [Fact]
        public void PredictGrowth_Valuezero()
        {
            // Arrange
            var predictionEngine = new PredictionEngine();
            var historicalData = new List<float> { 170000 };


            // Act
            var result = predictionEngine.GrowthModel(historicalData);

            // Assert
            Assert.Equal(0.0f, result);
        }
    }
}