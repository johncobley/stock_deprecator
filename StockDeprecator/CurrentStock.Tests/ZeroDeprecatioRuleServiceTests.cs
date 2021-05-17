using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using CurrentStock.Service;
using Xunit;

namespace CurrentStock.Tests
{
    public class ZeroDeprecatioRuleServiceTests
    {
        private ZeroDeprecationRule TestRule => new ZeroDeprecationRule
        {
            LastApplicableDay = 3
        };

        [Fact]
        public void SetsQualityToZeroWhenSellInIsGreaterThanLastApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 5,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.True(result);
            Assert.Equal(4, testStock.SellIn);
            Assert.Equal(0, testStock.Quality);
        }

        [Fact]
        public void SetsQualityToZeroWhenSellInIsEqualLastApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 3,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.True(result);
            Assert.Equal(2, testStock.SellIn);
            Assert.Equal(0, testStock.Quality);
        }

        [Fact]
        public void NoChangeWhenSellInIsLessThanLastApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 2,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.False(result);
            Assert.Equal(2, testStock.SellIn);
            Assert.Equal(5, testStock.Quality);
        }
    }
}
