using System.ComponentModel.DataAnnotations;

namespace StockDeprecatorMVC.Models
{
    public class CurrentStockViewModel
    {
        [Display(Name = "Current Stock List (update if required)")]
        public string StockList { get; set; }
    }
}
