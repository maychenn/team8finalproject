using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum PayeeType { CreditCard, Utilities, Rent, Mortgage, Other }

    public class Payee 
    {

        public Int32 PayeeID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public String Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [Display(Name = "Zip")]
        public String Zip { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Payee type is required")]
        public PayeeType Selection { get; set; }

        public List<Transaction> Transaction { get; set; }
        public List<PayBill> PayBill { get; set; }

        public Payee()
        {
            if (Transaction == null)
            {
                Transaction = new List<Transaction>();
            }
            if (PayBill == null)
            {
                PayBill = new List<PayBill>();
            }
        }
    }
}