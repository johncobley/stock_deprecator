using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentStock.DataAccess;
using CurrentStock.Service;
using Microsoft.AspNetCore.Mvc;
using StockDeprecatorMVC.Models;

namespace StockDeprecatorMVC.Controllers
{
    public class CurrentStockController : Controller
    {
        private readonly ICurrentStockRepository _currentStockRepository;

        private readonly IDeprecationService _deprecationService;

        public CurrentStockController(ICurrentStockRepository currentStockRepository, IDeprecationService deprecationService)
        {
            this._currentStockRepository = currentStockRepository;
            this._deprecationService = deprecationService;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            var currentStock = await this._currentStockRepository.Search(null);
            var stockList = string.Join(Environment.NewLine, currentStock.Select(item =>
                $"{item.StockTypeId} {item.SellIn} {item.Quality}"));
            return View(new CurrentStockViewModel { StockList = stockList });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public async Task<IActionResult> Index(CurrentStockViewModel currentStock)
        {
            await this._currentStockRepository.Save(currentStock.StockList);

            var currentStockItems = await this._currentStockRepository.Search(null);
            var stockList = string.Join("<br />", currentStockItems.Select(item =>
                $"{item.StockTypeId} {item.SellIn} {item.Quality}"));

            var deprecationResult = new List<string>();

            foreach (var stockItem in currentStockItems)
            {
                deprecationResult.Add(this._deprecationService.DeprecateItem(stockItem));
            }

            this._currentStockRepository.Save(currentStockItems);

            return View("Deprecate", new DeprecatedStockViewModel { StockList = stockList, DeprecatedStock = string.Join("<br />", deprecationResult) });
        }
    }
}
