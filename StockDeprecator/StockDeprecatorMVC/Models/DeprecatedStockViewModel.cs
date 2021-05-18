using System.ComponentModel.DataAnnotations;

namespace StockDeprecatorMVC.Models
{
    public class DeprecatedStockViewModel
    {
        [Display(Name = "Current Stock List")]
        public string StockList { get; set; }

        [Display(Name = "Deprecation Result")]
        public string DeprecatedStock { get; set; }
    }
}
