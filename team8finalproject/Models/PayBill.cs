using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using team8finalproject.Models;

namespace team8finalproject.Models
{
    public class PayBill
    {
        public Payee Payee { get; set; }
        public StandardAccount StandardAccount { get; set; }

        [Display(Name = "Payment Amount:")]
        public Decimal PaymentAmount { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
