using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System;

namespace team8finalproject.Models.ViewModels
{
    public class LoginViewModel 
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel 
    {

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //optional
        [Display(Name = "Middle Initial")]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public String StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [Display(Name = "Zip")]
        public String ZipCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

	public class ResetPasswordModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
		[Display(Name = "Confirm password")]
		public string ConfirmPassword { get; set; }

		[Required]
		public string ResetCode { get; set; }

	}

		public class EditViewModel

	{

		[Required(ErrorMessage = "First name is required.")]
		[Display(Name = "First Name")]
		public String FirstName { get; set; }

		//optional
		[Display(Name = "Middle Initial")]
		public String MiddleInitial { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[Display(Name = "Last Name")]
		public String LastName { get; set; }

		[Required(ErrorMessage = "Address is required.")]
		[Display(Name = "Address")]
		public String StreetAddress { get; set; }

		[Required(ErrorMessage = "City is required.")]
		[Display(Name = "City")]
		public String City { get; set; }

		[Required(ErrorMessage = "State is required.")]
		[Display(Name = "State")]
		public String State { get; set; }

		[Required(ErrorMessage = "Zip Code is required.")]
		[Display(Name = "Zip")]
		public String ZipCode { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		//NOTE: Here is the property for phone number
		[Required(ErrorMessage = "Phone number is required")]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Birthday is required")]
		[Display(Name = "Birthday")]
		[DataType(DataType.Date)]
		public DateTime Birthdate { get; set; }
	}

	public class IndexViewModel
    {

		[Required(ErrorMessage = "First name is required.")]
		[Display(Name = "First Name")]
		public String FirstName { get; set; }

		//optional
		[Display(Name = "Middle Initial")]
		public String MiddleInitial { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[Display(Name = "Last Name")]
		public String LastName { get; set; }

		[Required(ErrorMessage = "Address is required.")]
		[Display(Name = "Address")]
		public String StreetAddress { get; set; }

		[Required(ErrorMessage = "City is required.")]
		[Display(Name = "City")]
		public String City { get; set; }

		[Required(ErrorMessage = "State is required.")]
		[Display(Name = "State")]
		public String State { get; set; }

		[Required(ErrorMessage = "Zip Code is required.")]
		[Display(Name = "Zip")]
		public String ZipCode { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		//NOTE: Here is the property for phone number
		[Required(ErrorMessage = "Phone number is required")]
		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Birthday is required")]
		[Display(Name = "Birthday")]
		[DataType(DataType.Date)]
		public DateTime Birthdate { get; set; }


		public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String UserID { get; set; }
        public bool New { get; set; }
    }
}