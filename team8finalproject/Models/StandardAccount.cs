using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum AccountTypes { Checking, Savings }
    
    public class StandardAccount : Product
    {
        public Int32 StandardAccountID { get; set; }

        [Required(ErrorMessage = "Account Type is required.")]
        [Display(Name = "Account Type: ")]
        public AccountTypes AccountType { get; set; }

        [Required(ErrorMessage = "Account Balance is required.")]
        [Display(Name = "Account Balance: ")]
        public Decimal AccountBalance { get; set; }

        [Required(ErrorMessage = "Initial Deposit is required.")]
        [Display(Name = "Initial Deposit: ")]
        public Decimal InitialDeposit { get; set; }


        [Display(Name = "Overdraft: ")]
        public Decimal Overdraft { get; set; }

        public List<Transaction> Transaction { get; set; }
        public Product Product { get; set; }

        public StandardAccount()
        {
            // default names
            if (AccountType == AccountTypes.Checking && AccountName == null)
            {
                AccountName = "Longhorn Checking";
            }
            if (AccountType == AccountTypes.Savings && AccountName == null)
            {
                AccountName = "Longhorn Savings";
            }
            // initialize
            if (Transaction == null)
            {
                Transaction = new List<Transaction>();
            }
        }
    }
}
