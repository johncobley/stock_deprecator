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
        public void IncreasesQualityWhenSellInIsGreaterThanLastApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
            {
                LastApplicableDay = 3,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 5,
                Quality = 5
            };

            var service = new IncreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.True(result);
            Assert.Equal(4, testStock.SellIn);
            Assert.Equal(7, testStock.Quality);
        }

        [Fact]
        public void IncreasesQualityWhenSellInIsEqualLastApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
            {
                LastApplicableDay = 3,
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
            Assert.Equal(2, testStock.SellIn);
            Assert.Equal(7, testStock.Quality);
        }

        [Fact]
        public void NoChangeWhenSellInIsLessThanLastApplicableDay()
        {
            var testRule = new IncreasingDeprecationRule
            {
                LastApplicableDay = 3,
                Amount = 2
            };

            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 2,
                Quality = 5
            };

            var service = new IncreasingDeprecationRuleService();
            var result = service.RunRule(testRule, testStock);

            Assert.False(result);
            Assert.Equal(2, testStock.SellIn);
            Assert.Equal(5, testStock.Quality);
        }
    }
}
