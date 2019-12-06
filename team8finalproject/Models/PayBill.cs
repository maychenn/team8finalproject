using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using team8finalproject.Models;

namespace team8finalproject.Models
{
    public class PayBill
    {
        public Int32 PayBillID { get; set; }
        public Payee Payee { get; set; }
        public Product Product { get; set; }
        public AppUser User { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Payment Amount:")]
        public Decimal PaymentAmount { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public PayBill()
        {
            PaymentAmount = 0.0m;
            Date = DateTime.Now;
        }
    }
}
