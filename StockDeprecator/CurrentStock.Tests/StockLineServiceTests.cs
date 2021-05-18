using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrentStock.DataAccess;
using CurrentStock.Models;
using CurrentStock.Service;
using Moq;
using Xunit;

namespace CurrentStock.Tests
{
    public class StockLineServiceTests
    {
        private IStockTypeRepository DefaultStockTypeRepository()
        {
            var mockStockTypeRepo = new Mock<IStockTypeRepository>();
            mockStockTypeRepo
                .Setup(repo => repo.Get(It.IsAny<string>()))
                .Returns((string typeName) => Task.FromResult(new StockItemType { TypeName = typeName }));

            return mockStockTypeRepo.Object;
        }

        [Fact]
        public async void ParsesCorrectlyFormedStockLineWithSingleWordType()
        {
            var testLine = "Food 2 1";

            var stockLineService = new StockLineService(DefaultStockTypeRepository());
            var result = await stockLineService.ParseLine(testLine);

            Assert.Equal("Food", result.StockTypeId);
            Assert.Equal(2, result.SellIn);
            Assert.Equal(1, result.Quality);
        }

        [Fact]
        public async void ParsesCorrectlyFormedStockLineWithMultiWordType()
        {
            var testLine = "Toy Food 2 1";

            var stockLineService = new StockLineService(DefaultStockTypeRepository());
            var result = await stockLineService.ParseLine(testLine);

            Assert.Equal("Toy Food", result.StockTypeId);
            Assert.Equal(2, result.SellIn);
            Assert.Equal(1, result.Quality);
        }


        [Fact]
        public async void ParsesStockLineWithMissingQuality()
        {
            var testLine = "Vegan Food 2";

            var stockLineService = new StockLineService(DefaultStockTypeRepository());
            var result = await stockLineService.ParseLine(testLine);

            Assert.Equal("Vegan Food 2", result.StockTypeId);
            Assert.Equal(0, result.SellIn);
            Assert.Equal(0, result.Quality);
        }


        [Fact]
        public async void ParsesStockLineWithMissingQualityAndSellIn()
        {
            var testLine = "Cat Food";

            var stockLineService = new StockLineService(DefaultStockTypeRepository());
            var result = await stockLineService.ParseLine(testLine);

            Assert.Equal("Cat Food", result.StockTypeId);
            Assert.Equal(0, result.SellIn);
            Assert.Equal(0, result.Quality);
        }

        [Fact]
        public async void ParsesCorrectlyFormedStockLineAndAllocatesStockType()
        {
            var testLine = "Test Food 2 1";

            var mockStockTypeRepo = new Mock<IStockTypeRepository>();
            mockStockTypeRepo
                .Setup(repo => repo.Get(It.Is<string>(param => param == "Test Food")))
                .Returns((string typeName) => Task.FromResult(new StockItemType { TypeName = "Test Food" }));

            var stockLineService = new StockLineService(mockStockTypeRepo.Object);
            var result = await stockLineService.ParseLine(testLine);

            Assert.Equal("Test Food", result.StockType.TypeName);
        }
    }
}
