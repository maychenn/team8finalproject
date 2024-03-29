﻿using System;
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

		
        public Int32 AccountNumber { get; set; }

        [Display(Name = "Account Number")]
        public string AccNumber
        {
            get { return ("XXXXXX" + AccountNumber.ToString().Substring(6)); }
        }

        [Display(Name = "Account Name: ")]
		public string AccountName { get; set; }

		[Display(Name = "Account Status: ")]
        public AccountStatus AccountStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Total Balance: ")]
        public Decimal AccountBalance { get; set; }

        // Checking, Savings, IRA
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Initial Deposit: ")]
        public Decimal InitialDeposit { get; set; }

        // IRA Properties
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Total Contribution: ")]
        public Decimal Contribution { get; set; }

        // Stock Portfolio
        [Display(Name = "Balanced")]
        public Boolean Balanced { get; set; }

        // sum of (# of shares * current price)
        [Display(Name = "Stock Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal StockValue { get; set; }

        // gains + bonuses - fees + available cash
        [Display(Name = "Cash Value")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CashValue
        {
            get { return (Gains + Bonuses - Fees + AvailableCash); }
        }

        [Display(Name = "Gains")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Gains { get; set; }

        [Display(Name = "Bonuses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Bonuses { get; set; }

        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Fees { get; set; }

        // transfers go here
        [Display(Name = "Available Cash")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal AvailableCash { get; set; }

        //navigational properties
        public AppUser Customer { get; set; }
        public List<Transaction> Transaction { get; set; }
        public List<PortfolioDetail> PortfolioDetail { get; set; }

        public void CheckIfBalanced()
        {
            int a = 0;
            int b = 0;
            int c = 0;

            foreach (var item in PortfolioDetail)
            {
                if (item.Stock.StockType == StockType.Ordinary)
                {
                    a += 1;
                }
                if (item.Stock.StockType == StockType.IndexFund)
                {
                    b += 1;
                }
                if (item.Stock.StockType == StockType.MutualFund)
                {
                    c += 1;
                }
                if (a >= 2 && b >= 1 && c >= 1)
                {
                    Balanced = true;
                }
            }
        }

        public Product()
        {
            AccountStatus = AccountStatus.Inactive;
            AccountBalance = 0.0m;
            InitialDeposit = 0.0m;
            Balanced = false;
            AvailableCash = 0.0m;
            AccountNumber = 1000000000;
            StockValue = 0.0m;
            Gains = 0.0m;
            Bonuses = 0.0m;
            Fees = 0.0m;
            AvailableCash = 0.0m;
            Contribution = 0.0m;

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
