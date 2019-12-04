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
            ViewBag.AllAccounts = GetAllAccounts();

            return View();
        }

        // POST: PayBill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayBillID,PaymentAmount,Date,Name")] PayBill payBill, int SelectedPayee)
        {

            payBill.User = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                _context.Add(payBill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        private SelectList GetAllAccounts()
        {

            List<Product> accountList = _context.Products.ToList();
            SelectList accountSelection = new SelectList(accountList.OrderBy(m => m.ProductID), "ProductID", "AccountName", "AccountBalance");
            return accountSelection;
        }
    }
}
