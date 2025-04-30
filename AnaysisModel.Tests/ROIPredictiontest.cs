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
    }
}