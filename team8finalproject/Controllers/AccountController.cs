using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using team8finalproject.DAL;
using team8finalproject.Models;
using team8finalproject.Models.ViewModels;
using System.Net.Mail;
using System.Net;

namespace team8finalproject.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _db;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signIn;
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    //TODO: Add the rest of the custom user fields here
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    LastName = model.LastName,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    PhoneNumber = model.PhoneNumber,
                    Birthdate = model.Birthdate,
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role. This example adds the user to the customer role
                    await _userManager.AddToRoleAsync(user, "Customer");

                    Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    return RedirectToAction("Apply", "Product");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //GET: Account/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.Name;
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == id);

			//populate the view model
			ivm.Email = user.Email;
			ivm.Birthdate = user.Birthdate;
			ivm.City = user.City;
			ivm.LastName = user.LastName;
			ivm.FirstName = user.FirstName;
			ivm.State = user.State;
			ivm.MiddleInitial = user.MiddleInitial;
			ivm.StreetAddress = user.StreetAddress;
			ivm.ZipCode = user.ZipCode;
			ivm.PhoneNumber = user.PhoneNumber;
			ivm.HasPassword = true;
			ivm.UserID = user.Id;
			ivm.UserName = user.UserName;

			//send data to the view
			return View(ivm);
        }
        //GET: Account/Edit
		public ActionResult Edit()
		{
			EditViewModel evm = new EditViewModel();

			//get user info
			String id = User.Identity.Name;
			AppUser user = _db.Users.FirstOrDefault(u => u.UserName == id);

			//populate the view model
			ViewBag.Email = user.Email;
			ViewBag.Birthdate = user.Birthdate;
			evm.City = user.City;
			evm.LastName = user.LastName;
			evm.FirstName = user.FirstName;
			evm.State = user.State;
			evm.MiddleInitial = user.MiddleInitial;
			evm.StreetAddress = user.StreetAddress;
			evm.ZipCode = user.ZipCode;
			evm.PhoneNumber = user.PhoneNumber;

			//send data to the view
			return View(evm);
		}
        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            //fetch user
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            user.City = model.City;
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.State = model.State;
            user.MiddleInitial = model.MiddleInitial;
            user.StreetAddress = model.StreetAddress;
            user.ZipCode = model.ZipCode;
            user.PhoneNumber = model.PhoneNumber;

            await _db.SaveChangesAsync();
			return View("Index", "Account");
		}


		// GET: /Account/ForgotPassword
		[AllowAnonymous]
		public ActionResult ForgotPassword(string returnUrl)
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ForgotPassword(ResetPasswordModel model, string returnUrl)
		{


			if (!ModelState.IsValid)
			{
				return View(model);
			}
			AppUser userLoggedIn = await _userManager.FindByNameAsync(model.Email);
			var result = await _userManager.ChangePasswordAsync(userLoggedIn, model.NewPassword, model.ConfirmPassword);
			if (result.Succeeded)
			{
				return Redirect(returnUrl ?? "/");
			}
			AddErrors(result);
			return View(model);
		}

		// GET: /Account/ChangePassword
		public ActionResult ChangePassword()
		{
			return View();
		}

		//
		// POST: /Account/ChangePassword
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
			var result = await _userManager.ChangePasswordAsync(userLoggedIn, model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(userLoggedIn, isPersistent: false);
                EmailMessaging.SendEmail(userLoggedIn.Email, "Password Change Notice", "Your Password has been changed");
                return RedirectToAction("Index", "Home");
			}
			AddErrors(result);
			return View(model);
		}



		//GET:/Account/AccessDenied
		public ActionResult AccessDenied(String ReturnURL)
        {
            return View("Error", new string[] { "Access is denied" });
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}

