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
            return View(await _context.Products.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> CreateChecking([Bind("ProductID,ProductType,AccountName,InitialDeposit,AccountStatus")] Product product)
        {
            Product pd = new Product();
            pd.ProductType = product.ProductType;
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
                ViewBag.StatusUpdate = "You've successfully applied for an account!";
                pd.AccountStatus = AccountStatus.Active;
            }

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { productId = pd.ProductID });
            }
            return View(product);
        }
        
        // GET: Product/CreateSavings
        public IActionResult CreateSavings()
        {
            return View();
        }

        // POST: Product/CreateSavings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSavings([Bind("ProductID,ProductType,AccountName,InitialDeposit,AccountStatus")] Product product)
        {
            Product pd = new Product();

            //checks if the initial depo is > 5000, updates the status
            if (product.InitialDeposit > 5000)
            {
                ViewBag.StatusUpdate = "Your application must be approved by a manager.";
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully applied for a Savings Account!";
                pd.AccountStatus = AccountStatus.Active;
            }
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
            pd.InitialDeposit = product.InitialDeposit;
            pd.AccountBalance = product.AccountBalance + product.InitialDeposit;

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        // GET: Product/CreateIRA
        public IActionResult CreateIRA()
        {
            return View();
        }

        // POST: Product/CreatePortfolio
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePortfolio([Bind("ProductID,ProductType,AccountName,InitialDeposit,AccountStatus")] Product product)
        {
            Product pd = new Product();

            //checks if the initial depo is > 5000, updates the status
            if (product.InitialDeposit > 5000)
            {
                ViewBag.StatusUpdate = "Your application must be approved by a manager.";
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully applied for an IRA!";
                pd.AccountStatus = AccountStatus.Active;
            }
            pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
            pd.InitialDeposit = product.InitialDeposit;
            pd.AccountBalance = product.AccountBalance + product.InitialDeposit;

            if (product.AccountName != null)
            {
                pd.AccountName = product.AccountName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(pd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
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
    }
}
