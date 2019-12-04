using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum PortfolioStatus { Balanced, Unbalanced }

    public class Portfolio : Product
    {
        public Int32 PortfolioID { get; set; }

        [Required(ErrorMessage = "Cash Value is required.")]
        [Display(Name = "Cash Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CashValue { get; set; }

        // sum of (# of shares * current price)
        [Required(ErrorMessage = "Stock Value is required.")]
        [Display(Name = "Stock Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal StockValue { get; set; }

        [Display(Name = "Current Stock Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CurrentStockValue { get; set; }

        [Display(Name = "Gains")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Gains { get; set; }

        [Display(Name = "Bonuses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Bonuses { get; set; }

        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Fees { get; set; }

        [Display(Name = "Available Cash")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal AvailableCash { get; set; }

        /* The “value” (balance) of a portfolio includes:
        The current value of each stock, which is determined by multiplying the number of shares purchased by the current price of the stock.
        Any gains from previous sales of stock(see below).
        Any bonuses previously applied to the portfolio.
        Any available case in the cash-value portion of the portfolio
        Any other transactions that affect the cash-value portion of the portfolio.
        */

        public List<PortfolioDetail> PortfolioDetail { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Total Stock Portfolio Value: ")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TotalValue
        {
            get { return StockValue + CashValue; }
        }

        public PortfolioStatus PortfolioStatus { get; set; }

        public Portfolio()
        {
            if (PortfolioDetail == null)
            {
                PortfolioDetail = new List<PortfolioDetail>();
            }
        }
    }
}
