using System.Collections.Generic;
using CurrentStock.Models;
using CurrentStock.Service;
using Moq;
using Xunit;

namespace CurrentStock.Tests
{
    public class DeprecationServiceTests
    {
        private StockItem TestStockItem =>
            new StockItem
            {
                StockTypeId = "Food",
                StockType = new StockItemType
                {
                    TypeName = "Food",
                    DeprecationRules = new List<IDeprecationRule>
                    {
                        new ZeroDeprecationRule { FirstApplicableDay = 0},
                        new DecreasingDeprecationRule { FirstApplicableDay = 10 },
                        new IncreasingDeprecationRule { FirstApplicableDay = 5 },
                    }
                }
            };

        private DeprecationServices SetupRuleServices()
        {
            var decreasingService = new Mock<IDeprecationRuleService<DecreasingDeprecationRule>>();
            decreasingService
                .Setup(service => service.RunRule(It.IsAny<DecreasingDeprecationRule>(), It.IsAny<StockItem>()))
                .Returns(true)
                .Verifiable();

            var increasingService = new Mock<IDeprecationRuleService<IncreasingDeprecationRule>>();
            increasingService
                .Setup(service => service.RunRule(It.IsAny<IncreasingDeprecationRule>(), It.IsAny<StockItem>()))
                .Returns(true)
                .Verifiable();

            var zeroService = new Mock<IDeprecationRuleService<ZeroDeprecationRule>>();
            zeroService
                .Setup(service => service.RunRule(It.IsAny<ZeroDeprecationRule>(), It.IsAny<StockItem>()))
                .Returns(true)
                .Verifiable();

            return new DeprecationServices
            {
                DecreasingService = decreasingService,
                IncreasingService = increasingService,
                ZeroService = zeroService
            };
        }

        [Fact]
        public void SelectsDecreasingRuleWhenSellInBetweenTenAndSix()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 8;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food 7 0", result);

            services.DecreasingService
                .Verify(service => service.RunRule(It.IsAny<DecreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Once);

            services.IncreasingService
                .Verify(service => service.RunRule(It.IsAny<IncreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.ZeroService
                .Verify(service => service.RunRule(It.IsAny<ZeroDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);
        }

        [Fact]
        public void SelectsIncreasingRuleWhenSellInBetweenFiveAndOne()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 3;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food 2 0", result);

            services.DecreasingService
                .Verify(service => service.RunRule(It.IsAny<DecreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.IncreasingService
                .Verify(service => service.RunRule(It.IsAny<IncreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Once);

            services.ZeroService
                .Verify(service => service.RunRule(It.IsAny<ZeroDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);
        }

        [Fact]
        public void SelectsDecreasingRuleWhenSellInZero()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 0;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food -1 0", result);

            services.DecreasingService
                .Verify(service => service.RunRule(It.IsAny<DecreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.IncreasingService
                .Verify(service => service.RunRule(It.IsAny<IncreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.ZeroService
                .Verify(service => service.RunRule(It.IsAny<ZeroDeprecationRule>(), It.IsAny<StockItem>()), Times.Once);
        }

        [Fact]
        public void SelectsNoRuleWhenTypeHasNoRules()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 0;
            stockItem.StockType.DeprecationRules = null;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food 0 0", result);

            services.DecreasingService
                .Verify(service => service.RunRule(It.IsAny<DecreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.IncreasingService
                .Verify(service => service.RunRule(It.IsAny<IncreasingDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);

            services.ZeroService
                .Verify(service => service.RunRule(It.IsAny<ZeroDeprecationRule>(), It.IsAny<StockItem>()), Times.Never);
        }

        [Fact]
        public void SetsQualityToZeroIfNegative()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 8;
            stockItem.Quality = -2;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food 7 0", result);
            Assert.Equal(0, stockItem.Quality);
        }

        [Fact]
        public void SetsQualityToFiftyIfGreaterThanFifty()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 8;
            stockItem.Quality = 55;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("Food 7 50", result);
            Assert.Equal(50, stockItem.Quality);
        }

        [Fact]
        public void ReturnsNoSuchItemWhereNoType()
        {
            var services = this.SetupRuleServices();
            var deprecationService = new DeprecationService(
                services.DecreasingService.Object,
                services.IncreasingService.Object,
                services.ZeroService.Object);

            var stockItem = this.TestStockItem;
            stockItem.SellIn = 8;
            stockItem.Quality = 25;
            stockItem.StockType = null;

            var result = deprecationService.DeprecateItem(stockItem);

            Assert.Equal("NO SUCH ITEM", result);
        }

        private struct DeprecationServices
        {
            public Mock<IDeprecationRuleService<DecreasingDeprecationRule>> DecreasingService { get; set; }
            public Mock<IDeprecationRuleService<IncreasingDeprecationRule>> IncreasingService { get; set; }
            public Mock<IDeprecationRuleService<ZeroDeprecationRule>> ZeroService { get; set; }
        }
    }
}
