using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public class IRA: Product
    {
        public Int32 IRAID { get; set; }

        [Display(Name = "IRA Number: ")]
        public Int64 IRANumber { get; set; }

        [Display(Name = "Contributions this Year: ")]
        public Decimal ContributionsThisYear { get; set; }

        [Display(Name = "IRA Balance Year: ")]
        public Decimal IRABalance { get; set; }

        [Display(Name = "IRA Status: ")]
        public Boolean IRAStatus { get; set; }

        [Required(ErrorMessage = "Initial Deposit is required.")]
        [Display(Name = "Initial Deposit: ")]
        public Decimal InitialDeposit { get; set; }

        [Display(Name = "Account Balance: ")]
        public Decimal AccountBalance { get; set; }

        public List<Transaction> Transaction { get; set; }
        public Product Product { get; set; }

        public IRA()
        {
            // initialize
            if (Transaction == null)
            {
                Transaction = new List<Transaction>();
            }
        }
    }
}
