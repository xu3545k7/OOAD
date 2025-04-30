using Moq;
using Xunit;
using AnaysisModel;
using System.Collections.Generic;

namespace AnaysisModel.Tests
{
    public class AnalysisModuleTests
    {
        [Fact]
        public void CalculateGrowthPotential_ShouldReturnCorrectValue_WhenValidInputs()
        {
            var module = new AnalysisModule();
            var strategy = new Strategy { Description = "Increase Marketing Budget" };
            var result = module.CalculateGrowthPotential(strategy, new List<float> { 1f, 50f, 100f }, new List<int> { 1, 5, 10 });

            Assert.NotNull(result.Percentage);
            Assert.Equal(1.95066667f, result.Percentage); // 根據邏輯檢查百分比是否正確
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldThrowException_WhenStrategyIsNull()
        {
            var module = new AnalysisModule();

            Assert.Throws<ArgumentNullException>(() => module.CalculateGrowthPotential(null, new List<float> { 100f }, new List<int> { 10 }));
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldThrowException_WhenHistoricalCostIsEmpty()
        {
            // Arrange
            var analysisModule = new AnalysisModule();
            var strategy = new Strategy();
            List<float> emptyHistoricalCost = null;
            List<int> historicalHeadcount = new List<int> { 1, 2, 3 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                analysisModule.CalculateGrowthPotential(strategy, emptyHistoricalCost, historicalHeadcount));
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldReturnCorrectValue_ForOptimizeOperations()
        {
            var module = new AnalysisModule();
            var strategy = new Strategy { Description = "Optimize Operations" };
            var result = module.CalculateGrowthPotential(strategy, new List<float> { 1f, 50f, 100f }, new List<int> { 1, 5, 10 });

            Assert.Equal(2.7753334f, result.Percentage); // 根據邏輯檢查百分比是否正確
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldReturnCorrectValue_ForWithAdditionalRequirement()
        {
            var module = new AnalysisModule();
            var strategy = new Strategy { Description = "With Additional Requirement" };
            var result = module.CalculateGrowthPotential(strategy, new List<float> { 1f, 50f, 100f }, new List<int> { 1, 5, 10 });

            Assert.Equal(2.30133343f, result.Percentage); // 根據邏輯檢查百分比是否正確
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldReturnCorrectValue_Fordefault()
        {
            var module = new AnalysisModule();
            var strategy = new Strategy { Description = "Whatever" };
            var result = module.CalculateGrowthPotential(strategy, new List<float> { 1f, 50f, 100f }, new List<int> { 1, 5, 10 });

            Assert.Equal(1.10000002f, result.Percentage); // 根據邏輯檢查百分比是否正確
        }

        [Fact]
        public void CalculateGrowthPotential_ShouldThrowException_WhenStrategyDescriptionIsNull()
        {
            var module = new AnalysisModule();
            var strategy = new Strategy { Description = null };
            Assert.Throws<ArgumentNullException>(() =>
                module.CalculateGrowthPotential(strategy, new List<float> { 1f, 50f, 100f }, new List<int> { 1, 5, 10 }));
        }
    }
}