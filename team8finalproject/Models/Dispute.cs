using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public enum DisputeStatus { Submitted, Accepted, Rejected, Adjusted }

    public class Dispute
    {
        public Transaction Transaction { get; set; }

        public Int32 DisputeID { get; set; }

        [Display(Name = "Correct Transaction Amount: ")]
        public Decimal NewAmount { get; set; }

        [Display(Name = "Dispute Comment: ")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Select an option below.")]
        public bool Delete { get; set; }

        [Display(Name = "Manager Comment: ")]
        public String ManagerComment { get; set; }

        [Display(Name = "Dispute Status: ")]
        public DisputeStatus DisputeStatus { get; set; }

        public Dispute()
        {
            if (Delete == true)
            {
                NewAmount = 0.00m;
            }
        }
    }
}
