using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum ProductTypes { Checking, Savings, IRA, Portfolio }
    public enum AccountStatus { Active, Inactive }

    public class Product
    {
        public Int32 ProductID { get; set; }

		[Required(ErrorMessage = "Account Type is required.")]
		[Display(Name = "Account Type: ")]
		public ProductTypes ProductType { get; set; }

		[Display(Name = "Account Number")]
        [DisplayFormat(DataFormatString = "XXXXXX####")]
        public Int32 AccountNumber { get; set; }

		[Display(Name = "Account Name: ")]
		public string AccountName { get; set; }

		[Display(Name = "Account Status: ")]
        public AccountStatus AccountStatus { get; set; }

        [Display(Name = "Initial Deposit: ")]
        public Decimal InitialDeposit { get; set; }

        [Display(Name = "Total Contribution: ")]
        public Decimal Contribution { get; set; }

        [Display(Name = "Total Balance: ")]
        public Decimal AccountBalance { get; set; }

        //navigational properties
        public AppUser Customer { get; set; }
        public List<Transaction> Transaction { get; set; }
        public List<PortfolioDetail> PortfolioDetail { get; set; }

        public Product()
        { 
            if (AccountBalance == null)
            {
                AccountBalance = 0.0m;
            }
            
            // defaults
            AccountStatus = AccountStatus.Inactive;
            if (Transaction == null)
            {
                Transaction = new List<Transaction>();
            }
            if (PortfolioDetail == null)
            {
                PortfolioDetail = new List<PortfolioDetail>();
            }
        }
    }
}
