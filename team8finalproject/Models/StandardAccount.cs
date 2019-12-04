using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum AccountTypes { Checking, Savings, IRA, Portfolio }
    public enum AccountStatus { Active, Inactive}

    public class StandardAccount
    {
        public Int32 StandardAccountID { get; set; }

        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; }

        [Required(ErrorMessage = "Account Type is required.")]
        [Display(Name = "Account Type: ")]
        public AccountTypes AccountType { get; set; }

        [Required(ErrorMessage = "Account Name is required.")]
        [Display(Name = "Account Name: ")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Account Balance is required.")]
        [Display(Name = "Account Balance: ")]
        public Decimal AccountBalance { get; set; }

        [Display(Name = "Overdraft: ")]
        public Decimal Overdraft { get; set; }

        [Required(ErrorMessage = "Account Status is required.")]
        [Display(Name = "Account Status: ")]
        public AccountStatus AccountStatus { get; set; }

        public AppUser Customer { get; set; }

        public List<Transaction> Transaction { get; set; }
        public Portfolio Portfolio { get; set; }
        public IRA IRA { get; set; }
        
        public StandardAccount()
        {
            // default status
            AccountStatus = AccountStatus.Inactive;

            if (Transaction == null)
            {
                Transaction = new List<Transaction>();
            }
            // default names
            if (AccountType == AccountTypes.Checking && AccountName == null)
            {
                AccountName = "Longhorn Checking";
            }
            if (AccountType == AccountTypes.Savings && AccountName == null)
            {
                AccountName = "Longhorn Savings";
            }
        }
    }
}
