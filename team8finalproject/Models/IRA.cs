using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace team8finalproject.Models
{
    public class IRA: StandardAccount
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


        public StandardAccount StandardAccount { get; set;}

    }
}
