using System;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models.ViewModels
{
    public enum AmountRanges {
        [Display(Name = "$0-100")] Low,
        [Display(Name = "$100-200")] Medium,
        [Display(Name = "$200-300")] High,
        [Display(Name = "$300+")] Highest,
        [Display(Name = "Custom Amount Range")] Custom }

    public enum DateRanges
    {
        [Display(Name = "Last 15 Days")] Last15,
        [Display(Name = "Last 30 Days")] Last30,
        [Display(Name = "Last 60 Days")] Last60,
        [Display(Name = "All Available")] All,
        [Display(Name = "Custom Date Range")] Custom
    }

    public enum AccountType { Checkings, Savings, Portfolio, IRA }

    public class SearchViewModel

    {
        [Display(Name = "Transaction Number:")]
        public string TransactionNumber { get; set; }

        [Display(Name = "Transaction Type:")]
        public TransactionTypes TransactionType { get; set; }

        [Display(Name = "Search Description:")]
        public string TransactionDescription { get; set; }

        [Display(Name = "Transaction Amount Range:")]
        public AmountRanges? AmountRange { get; set; }

        [Display(Name = "Transaction Amount Lower Bound:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal? LowerLimit { get; set; }

        [Display(Name = "Transaction Amount Upper Bound:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal? UpperLimit { get; set; }

        [Display(Name = "Transaction Date Range:")]
        public DateRanges? DateRange { get; set; }

        [Display(Name = "Beginning Date:")]
        [DataType(DataType.Date)]
        public DateTime? BeginningDate { get; set; }

        [Display(Name = "Ending Date:")]
        [DataType(DataType.Date)]
        public DateTime? EndingDate { get; set; }

    }
}
