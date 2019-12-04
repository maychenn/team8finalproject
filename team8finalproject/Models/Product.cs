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

		[Required(ErrorMessage = "Product Type is required.")]
		[Display(Name = "Product Type: ")]
		public ProductTypes ProductType { get; set; }

		[Display(Name = "Account Number")]
		public Int32 AccountNumber { get; }

		[Required(ErrorMessage = "Account Name is required.")]
		[Display(Name = "Account Name: ")]
		public string AccountName { get; set; }

		[Display(Name = "Account Status: ")]
        public AccountStatus AccountStatus { get; set; }

        //navigational properties
        public AppUser Customer { get; set; }
        public List<Transaction> Transaction { get; set; }

        public Product()
        {
            // default status
            AccountStatus = AccountStatus.Inactive;

        }
    }
}
