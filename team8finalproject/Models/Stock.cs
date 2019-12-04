using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{

    public enum StockType
    {
        [Display(Name = "Ordinary Stock")] Ordinary,
        [Display(Name = "Index Fund")] IndexFund,
        [Display(Name = "Exchange Transfer Funds")] ETF,
        [Display(Name = "Mutual Fund")] MutualFund,
        [Display(Name = "Future Shares")] Future
    }

    public class Stock
    {
       
        public Int32 StockID { get; set; }

        [Required(ErrorMessage = "Ticker Symbol is required.")]
        [Display(Name = "Ticker Symbol")]
        public string Ticker { get; set; }

        [Required(ErrorMessage = "Stock Type is required.")]
        [Display(Name = "Stock Type")]
        public StockType StockType { get; set; }

        [Required(ErrorMessage = "Stock Name is required")]
        [Display(Name = "Stock Name")]
        public string StockName { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Display(Name = "Stock Price")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Fee is required.")]
        [Display(Name = "Stock Fee")]
        public Decimal Fee { get; set; }

        public Stock()
        {
        }
    }
}
