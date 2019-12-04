﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum TransactionTypes { Deposit, Withdrawal, Fee, Transfer, [Display(Name = "Stock Purchase")] StockPurchase }

    public class Transaction
    { 
        public Int32 TransactionID { get; set; }

        [Display(Name = "Transaction Number")]
        public Int32 Number { get; set; }

        [Display(Name = "Transaction Description")]
        public String Description { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Transaction Amount")]
        public Decimal Amount { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionTypes TransactionType { get; set; }

        [Display(Name = "Account")]
        public StandardAccount Account { get; set; }

        public AppUser AppUser { get; set; }
        public Payee Payee { get; set;  }
        public List<Dispute> Dispute { get; set; }
       
        public Transaction()
        {
            if (Dispute == null)
            {
                Dispute = new List<Dispute>();
            }
        }
    }
}