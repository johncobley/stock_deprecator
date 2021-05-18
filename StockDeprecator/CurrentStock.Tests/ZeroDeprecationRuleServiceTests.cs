using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using CurrentStock.Service;
using Xunit;

namespace CurrentStock.Tests
{
    public class ZeroDeprecationRuleServiceTests
    {
        private ZeroDeprecationRule TestRule => new ZeroDeprecationRule
        {
            FirstApplicableDay = 2
        };

        [Fact]
        public void SetsQualityToZeroWhenSellInIsLessThanFirstApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 1,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.True(result);
            Assert.Equal(0, testStock.Quality);
        }

        [Fact]
        public void SetsQualityToZeroWhenSellInIsEqualFirstApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 2,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.True(result);
            Assert.Equal(0, testStock.Quality);
        }

        [Fact]
        public void NoChangeWhenSellInIsGreaterThanFirstApplicableDay()
        {
            var testStock = new StockItem
            {
                StockTypeId = "Stock 1",
                SellIn = 3,
                Quality = 5
            };

            var service = new ZeroDeprecationRuleService();
            var result = service.RunRule(this.TestRule, testStock);

            Assert.False(result);
            Assert.Equal(5, testStock.Quality);
        }
    }
}
