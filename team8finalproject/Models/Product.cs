﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum ProductTypes { StandardAccount, IRA, Portfolio }
    public enum AccountStatus { Active, Inactive }

    public class Product
    {
        public Int32 ProductID { get; set; }

        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; }

        [Required(ErrorMessage = "Product Type is required.")]
        [Display(Name = "Product Type: ")]
        public ProductTypes ProductType { get; set; }

        [Required(ErrorMessage = "Account Name is required.")]
        [Display(Name = "Account Name: ")]
        public string AccountName { get; set; }


        [Required(ErrorMessage = "Account Status is required.")]
        [Display(Name = "Account Status: ")]
        public AccountStatus AccountStatus { get; set; }

        public AppUser Customer { get; set; }

        public Product()
        {
            // default status
            AccountStatus = AccountStatus.Inactive;

        }
    }
}