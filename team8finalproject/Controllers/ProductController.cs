using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using team8finalproject.DAL;
using team8finalproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace team8finalproject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ProductController(AppDbContext context, RoleManager<IdentityRole> roleManager, IServiceProvider service)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
			List<Product> Products = new List<Product>();
			if (User.IsInRole("Admin"))
			{
				Products = _context.Products.ToList();
			}
			else //user is customer
			{
				Products = _context.Products.Where(r => r.Customer.UserName == User.Identity.Name).ToList();
			}

			return View(Products);
		}

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Transaction)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

		// GET: Product/Details/5
		public async Task<IActionResult> DetailsPortfolio()
		{
			var portfolio = _context.Products
                .Include(p=>p.PortfolioDetail)
				.ThenInclude(p => p.Stock)
				.Where(p => p.Customer.UserName == User.Identity.Name)
                .Where(p => p.ProductType == ProductTypes.Portfolio).ToList();

            if (portfolio.Count != 0)
			{
				Product product = portfolio[0];
                
				return View(product);
			}

			return View();
		}

		// GET: Product/Apply
		public IActionResult Apply()
        {
            return View();   
        }

        // POST: Product/Apply
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(String dummy)
        {
            return View();

        }
        
        // GET: Product/CreateChecking
        public IActionResult CreateChecking()
        {
            return View("CreateChecking");
        }

        // POST: Product/CreateChecking
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChecking([Bind("ProductID,ProductType,AccountNumber,AccountName,InitialDeposit,AccountBalance,AccountStatus")] Product product)
        {
            Product pd = new Product();
            pd.ProductType = ProductTypes.Checking;

            // gets next number
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
            pd.InitialDeposit = product.InitialDeposit;
            pd.AccountBalance = product.InitialDeposit;

            pd.Customer = await _userManager.FindByNameAsync(User.Identity.Name);

            //checks if the initial deposit is > 5000, updates the status
            if (product.InitialDeposit > 5000)
            {
                ViewBag.StatusUpdate = "Your application must be approved by a manager.";
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully applied for a Checking account!";
                pd.AccountStatus = AccountStatus.Active;
            }

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }
            else
            {
                pd.AccountName = "Longhorn Checking";
            }
            if (product.InitialDeposit < 0)
            {
                return View(product);

            }
            _context.Add(pd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Product", new { id = pd.ProductID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { productId = pd.ProductID });
            }*/
            
        }

        // GET: Product/CreateSavings
        public IActionResult CreateSavings()
        {
            return View("CreateSavings");
        }

        // POST: Product/CreateSavings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSavings([Bind("ProductID,ProductType,AccountNumber,AccountName,InitialDeposit,AccountBalance,AccountStatus")] Product product)
        {
            Product pd = new Product();
            pd.ProductType = ProductTypes.Savings;
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
            pd.InitialDeposit = product.InitialDeposit;
            pd.AccountBalance = product.InitialDeposit;

            pd.Customer = await _userManager.FindByNameAsync(User.Identity.Name);

            //checks if the initial deposit is > 5000, updates the status
            if (product.InitialDeposit > 5000)
            {
                ViewBag.StatusUpdate = "Your application must be approved by a manager.";
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully applied for a Savings account!";
                pd.AccountStatus = AccountStatus.Active;
            }

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }
            else
            {
                pd.AccountName = "Longhorn Savings";
            }
            if (product.InitialDeposit < 0)
            {
                return View(product);

            }
            _context.Add(pd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Product", new { id = pd.ProductID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { productId = pd.ProductID });
            }*/

        }
        // GET: Product/CreateIRA
        public IActionResult CreateIRA()
        {
            return View("CreateIRA");
        }

        // POST: Product/CreateIRA
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIRA([Bind("ProductID,ProductType,AccountNumber,AccountName,InitialDeposit,AccountBalance,AccountStatus")] Product product)
        {
            Product pd = new Product();
            pd.ProductType = ProductTypes.IRA;
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
            pd.InitialDeposit = product.InitialDeposit;
            pd.AccountBalance = product.InitialDeposit;
            //contribution = initial deposit

            pd.Customer = await _userManager.FindByNameAsync(User.Identity.Name);

            //checks if the initial deposit is > 5000, updates the status
            if (product.InitialDeposit > 5000)
            {
                ViewBag.StatusUpdate = "Your application must be approved by a manager.";
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully applied for an IRA!";
                pd.AccountStatus = AccountStatus.Active;
            }

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }
            else
            {
                pd.AccountName = "Longhorn IRA";
            }
            if (product.InitialDeposit < 0)
            {
                return View(product);

            }
            _context.Add(pd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Product", new { id = pd.ProductID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { productId = pd.ProductID });
            }*/

        }
        // GET: Product/CreatePortfolio
        public IActionResult CreatePortfolio()
        {
            return View("CreatePortfolio");
        }

        // POST: Product/CreateIRA
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePortfolio([Bind("ProductID,ProductType,AccountNumber,AccountName,InitialDeposit,AccountBalance,AccountStatus")] Product product)
        {
            Product pd = new Product();
            pd.ProductType = ProductTypes.Portfolio;
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);

            pd.Customer = await _userManager.FindByNameAsync(User.Identity.Name);
			var pt = _context.Products.Where(p => p.Customer.UserName == User.Identity.Name)
				.Where(p => p.ProductType == ProductTypes.Portfolio).ToList();
			if (pt.Count != 0)
			{
				//ViewBag.ErrorMessage = "You already have a stock portfolio account!";
				TempData["ErrorMessage"] = "You already have a stock portfolio account!";
			}
			// user doesn't have a stock portfolio
			return RedirectToAction("CreatePortfolio", "Product");
			//checks if the initial deposit is > 5000, updates the status
			ViewBag.StatusUpdate = "You've successfully applied for a Portfolio!";
			//checks if the initial deposit is > 5000, updates the status
			ViewBag.StatusUpdate = "You've successfully applied for a Portfolio!";
            pd.AccountStatus = AccountStatus.Active;

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }
            else
            {
                pd.AccountName = "Longhorn Portfolio";
            }
            _context.Add(pd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "PortfolioDetail", new { id = pd.ProductID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { productId = pd.ProductID });
            }*/

        }
        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SelectStatus = Enum.GetValues(typeof(AccountStatus)).Cast<AccountStatus>();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductType,AccountName,AccountStatus")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    
        [Authorize(Roles = "Manager")]
        public ActionResult ProcessBalancedPortfolios(AppDbContext _context)
        {
            var AllPortfolios = _context.Products.Where(p => p.ProductType == ProductTypes.Portfolio);

            foreach (var s in AllPortfolios)
            {
                s.CheckIfBalanced();
                // add bonus as a transaction
                if (s.Balanced)
                {
                    Decimal Bonus = s.StockValue * .1m;
                    s.Bonuses += Bonus;

                    Transaction transaction = new Transaction();
                    transaction.Amount = Bonus;
                    transaction.Product = s;
                    transaction.Description = "Balanced Portfolio Bonus";
                    transaction.Date = DateTime.Now;
                    transaction.TransactionType = TransactionTypes.Bonus;
                    transaction.TransactionStatus = TransactionStatus.Approved;

                    _context.Transactions.Add(transaction);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("ProcessStockPortfoliosConfirmationMessage", "StockPortfolios");
        }
        
    }
}
