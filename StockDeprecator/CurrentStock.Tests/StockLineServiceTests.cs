using System;
using CurrentStock.Models;
using CurrentStock.Service;
using Xunit;

namespace CurrentStock.Tests
{
    public class StockLineServiceTests
    {
        [Fact]
        public void ParsesCorrectlyFormedStockLineWithSingleWordType()
        {
            var testLine = "Food 2 1";
            var expected = new StockItem
            {
                StockTypeId = "Food",
                SellIn = 2,
                Quality = 1
            };

            var stockLineService = new StockLineService();
            var result = stockLineService.ParseLine(testLine);

            Assert.True(expected.StockTypeId.Equals(result.StockTypeId));
        }

        [Fact]
        public void ParsesCorrectlyFormedStockLineWithMultiWordType()
        {
            var testLine = "Toy Food 2 1";
            var expected = new StockItem
            {
                StockTypeId = "Toy Food",
                SellIn = 2,
                Quality = 1
            };

            var stockLineService = new StockLineService();
            var result = stockLineService.ParseLine(testLine);

            Assert.True(expected.StockTypeId.Equals(result.StockTypeId));
        }


        [Fact]
        public void ParsesStockLineWithMissingQuality()
        {
            var testLine = "Vegan Food 2";
            var expected = new StockItem
            {
                StockTypeId = "Vegan Food 2",
                SellIn = 0,
                Quality = 0
            };

            var stockLineService = new StockLineService();
            var result = stockLineService.ParseLine(testLine);

            Assert.True(expected.StockTypeId.Equals(result.StockTypeId));
        }


        [Fact]
        public void ParsesStockLineWithMissingQualityAndSellIn()
        {
            var testLine = "Cat Food";
            var expected = new StockItem
            {
                StockTypeId = "Cat Food",
                SellIn = 0,
                Quality = 0
            };

            var stockLineService = new StockLineService();
            var result = stockLineService.ParseLine(testLine);

            Assert.True(expected.StockTypeId.Equals(result.StockTypeId));
        }
    }
}
