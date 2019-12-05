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

namespace team8finalproject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private RoleManager<IdentityRole> _roleManager;

        public ProductController(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
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
        public IActionResult Apply(String productType)
        {
            Product pd = new Product();
            if (productType == "Checking")
            {
                pd.ProductType = ProductTypes.Checking;
            }
            if (productType == "Savings")
            {
                pd.ProductType = ProductTypes.Savings;
            }
            if (productType == "Portfolio")
            {
                pd.ProductType = ProductTypes.Portfolio;
            }
            if (productType == "IRA")
            {
                pd.ProductType = ProductTypes.IRA;
            }
            return View("Create", pd);

        }

        // GET: Product/Create
        public IActionResult Create(String productType)
        {
            Product pd = new Product();

            if (productType == "Checking")
            {
                pd.ProductType = ProductTypes.Checking;
            }
            else if(productType == "Savings")
            {
                pd.ProductType = ProductTypes.Savings;
            }
            else if (productType == "IRA")
            {
                pd.ProductType = ProductTypes.IRA;
            }
            else
            {
                pd.ProductType = ProductTypes.Portfolio;
            }
            return View(pd);
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductType,AccountName,InitialDeposit,AccountStatus")] Product product)
        {
            Product pd = new Product();
            if (product.ProductType == ProductTypes.Checking || product.ProductType == ProductTypes.Savings)
            {
                pd.AccountNumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);
                pd.InitialDeposit = product.InitialDeposit;
                pd.AccountBalance = product.AccountBalance + product.InitialDeposit;
                if(product.AccountName != null)
                {
                    pd.AccountName = product.AccountName;
                }
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
