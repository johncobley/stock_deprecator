using CurrentStock.Models;
using CurrentStock.Service;
using Xunit;

namespace CurrentStock.Tests
{
    public class DecreasingDeprecationRuleServiceTests
    {
        [Fact]
        public void DecreasesQualityWhenSellInIsLessThanFirstApplicableDay()
        {
            var testRule = new DecreasingDeprecationRule
            {
                FirstApplicableDay = 3,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 2,
                Quality = 5
            };

            var service = new DecreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(3, testStock.Quality);
        }

        [Fact]
        public void DecreasesQualityWhenSellInIsEqualFirstApplicableDay()
        {
            var testRule = new DecreasingDeprecationRule
            {
                FirstApplicableDay = 3,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 3,
                Quality = 5
            };

            var service = new DecreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(3, testStock.Quality);
        }

        [Fact]
        public void NoChangeWhenSellInIsGreaterThanFirstApplicableDay()
        {
            var testRule = new DecreasingDeprecationRule
            {
                FirstApplicableDay = 3,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 4,
                Quality = 5
            };

            var service = new DecreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.False(result);
            Assert.Equal(5, testStock.Quality);
        }

        [Fact]
        public void DoubleChangeWhenSellInIsLessZero()
        {
            var testRule = new DecreasingDeprecationRule
            {
                FirstApplicableDay = int.MaxValue,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = -1,
                Quality = 5
            };

            var service = new DecreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(1, testStock.Quality);
        }
    }
}
