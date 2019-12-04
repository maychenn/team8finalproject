using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//TODO: Make this namespace match your project
namespace team8finalproject.Models
{
    public class AppUser : IdentityUser
    {
        //Identity creates several of the important fields for you
        //Full documentation of the IdentityUser class can be found at
        //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityuser?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-2.1

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        // optional
        [Display(Name = "Middle Initial")]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Street Address is required.")]
        [Display(Name = "Street Address")]
        public String StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [Display(Name = "Zip Code")]
        public String ZipCode { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [Display(Name = "Birthday")]
        public DateTime Birthdate { get; set; }

        public Boolean New { get; set; }

        public Int32 Age { get {
                DateTime now = DateTime.Today;
                Int32 age = now.Year - Birthdate.Year;
                return age;
         } }

        public List<StandardAccount> StandardAccount { get; set; }
        public Portfolio Portfolio { get; set; }
        public IRA IRA { get; set; }
        public List<PayBill> PayBill { get; set; }

        public AppUser()
        {
            // new user
            if (StandardAccount == null)
            {
                StandardAccount = new List<StandardAccount>();
                New = true;
            }
            // user with no accounts
            else if (StandardAccount.Count == 0)
            {
                New = true;
            }
            else
            {
                New = false;
            }

        }


    }

}