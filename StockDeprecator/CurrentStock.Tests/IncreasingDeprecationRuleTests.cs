using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using CurrentStock.Service;
using Xunit;

namespace CurrentStock.Tests
{
    public class IncreasingDeprecationRuleTests
    {
        [Fact]
        public void IncreasesQualityWhenSellInIsLessThanFirstApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
            {
                FirstApplicableDay = 5,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 3,
                Quality = 5
            };

            var service = new IncreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(7, testStock.Quality);
        }

        [Fact]
        public void IncreasesQualityWhenSellInIsEqualLastApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
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

            var service = new IncreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(7, testStock.Quality);
        }

        [Fact]
        public void NoChangeWhenSellInIsGreaterThanFirstApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
            {
                FirstApplicableDay = 2,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 3,
                Quality = 5
            };

            var service = new IncreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.False(result);
            Assert.Equal(5, testStock.Quality);
        }
    }
}
