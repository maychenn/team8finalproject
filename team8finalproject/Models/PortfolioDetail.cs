using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public class PortfolioDetail
    {
        public Int32 PortfolioDetailID { get; set; }

        [Required(ErrorMessage = "Number of Shares is required.")]
        [Display(Name = "Number of Shares")]
        [Range(1, 1000, ErrorMessage = "Number of Shares must be between 1 and 1000")]
        public Int32 NumShares { get; set; }

        [Required(ErrorMessage = "Current Stock Price is required.")]
        [Display(Name = "Current Stock Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal StockPrice { get; set; }

    }
}
