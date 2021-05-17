using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentStock.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockDeprecatorMVC.Models;

namespace StockDeprecatorMVC.Controllers
{
    public class CurrentStockController : Controller
    {
        private ICurrentStockRepository _currentStockRepository;

        public CurrentStockController(ICurrentStockRepository currentStockRepository)
        {
            this._currentStockRepository = currentStockRepository;
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
            return RedirectToAction("Index");
        }
    }
}
