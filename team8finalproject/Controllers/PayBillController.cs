using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using team8finalproject.DAL;
using team8finalproject.Models;
using Microsoft.AspNetCore.Identity;

namespace team8finalproject.Controllers
{
    public class PayBillController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PayBillController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PayBill
        public async Task<IActionResult> Index()
        {
            List<PayBill> PayBills = new List<PayBill>();
            if (User.IsInRole("Admin"))
            {
                PayBills = _context.PayBills.Include(r => r.Payee).ToList();
            }
            else //user is customer
            {
                PayBills = _context.PayBills.Where(r => r.User.UserName == User.Identity.Name).Include(r => r.Payee).ToList();
            }

            return View(PayBills);
        }

        // GET: PayBill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payBill = await _context.PayBills
                .FirstOrDefaultAsync(m => m.PayBillID == id);
            if (payBill == null)
            {
                return NotFound();
            }

            return View(payBill);
        }

        // GET: PayBill/Create
        public IActionResult Create()
        {
            ViewBag.AllPayees = GetAllPayees();
            ViewBag.Accounts = FindAccounts();

            return View();
        }

        // POST: PayBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayBillID,PaymentAmount,Date,Name,AccountBalance,PayeeName")] PayBill payBill, int SelectedPayee, int SelectedAccount)
        {

            //find the correct payee 
            Payee payee = _context.Payees.Find(SelectedPayee);
            payBill.Payee = payee;

            //find the correct account
            Product product = _context.Products.Find(SelectedAccount);
            payBill.Product = product;

            //subtract bill from account balance 
            payBill.Product.AccountBalance = payBill.Product.AccountBalance - payBill.PaymentAmount;
            if (payBill.Product.AccountBalance < 50)
            {
                payBill.Product.AccountBalance = payBill.Product.AccountBalance + payBill.PaymentAmount;
                ViewBag.Error = TempData["error"];
                return View(payBill);
            }

            if (ModelState.IsValid)
            {
                _context.Add(payBill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","PayBill");
            }
            return View(payBill);
        }

        // GET: PayBill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payBill = await _context.PayBills.FindAsync(id);
            if (payBill == null)
            {
                return NotFound();
            }
            return View(payBill);
        }

        // POST: PayBill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PayBillID,PaymentAmount,Date")] PayBill payBill)
        {
            if (id != payBill.PayBillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayBillExists(payBill.PayBillID))
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
            return View(payBill);
        }

        // GET: PayBill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payBill = await _context.PayBills
                .FirstOrDefaultAsync(m => m.PayBillID == id);
            if (payBill == null)
            {
                return NotFound();
            }

            return View(payBill);
        }

        // POST: PayBill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payBill = await _context.PayBills.FindAsync(id);
            _context.PayBills.Remove(payBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayBillExists(int id)
        {
            return _context.PayBills.Any(e => e.PayBillID == id);
        }


        private SelectList GetAllPayees()
        {
            List<Payee> payeeList = _context.Payees.ToList();

            SelectList payeeSelection = new SelectList(payeeList.OrderBy(m => m.PayeeID), "PayeeID","Name");
            return payeeSelection;
        }

        public SelectList FindAccounts()
        {
            var query = from p in _context.Products
                        select p;
            {
                query = query.Where(p => p.ProductType == ProductTypes.Checking || p.ProductType == ProductTypes.Savings);
            }

            List<Product> accountList = query.ToList();
            SelectList accountSelection = new SelectList(accountList, "AccountName", "AccountName");
            return accountSelection;
        }
    }
}
