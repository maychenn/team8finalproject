using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using team8finalproject.DAL;
using team8finalproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace team8finalproject.Controllers
{
    public class IRAController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IRAController(AppDbContext context)
        {
            _context = context;
        }

        // GET: IRA
        public async Task<IActionResult> Index()
        {
            return View(await _context.IRAs.ToListAsync());
        }

        // GET: IRA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iRA = await _context.IRAs
                .FirstOrDefaultAsync(m => m.IRAID == id);
            if (iRA == null)
            {
                return NotFound();
            }

            return View(iRA);
        }

        // GET: IRA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IRA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IRAID,ContributionsThisYear,StandardAccountID,AccountType,AccountName,AccountBalance,Overdraft,AccountStatus")] IRA iRA)
        {

            //Generates IRA Number
            iRA.IRANumber = Utilities.GenerateAccountNumber.GetNextAccountNumber(_context);

            //Connects User to the IRA
            iRA.Customer = await _userManager.FindByNameAsync(User.Identity.Name);

            //Restricts User to be less than 70
            if (iRA.Customer.Age < 70)
            {


                _context.Add(iRA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(iRA));



            }
    

            else

            {

                return RedirectToAction("Error", new string[] { "You must be less than 70 years old to create an IRA Account" });

            }

        }



        // GET: IRA/Contribute
        public IActionResult Contribute()
        {
            return View();
        }


        //Post: IRA/Contribute
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contribute([Bind("IRAID,ContributionsThisYear,StandardAccountID,AccountType,AccountName,AccountBalance,Overdraft,AccountStatus")] IRA iRA)
        {

            AppUser user = _context.Users.Find(iRA.Product.ProductID);
            iRA.Customer = user;

            if (user.IRA != null)
            {
                return View("Error", new string[] { "You have already created an IRAccount." });
            }

            else
            {
                if (ModelState.IsValid)
                {
                    iRA.AccountName = "Longhorn IRA";

                    decimal MaxContribution = 5000;

                    Transaction transaction = new Transaction();
                    transaction.TransactionType = TransactionTypes.Deposit;
                    transaction.Amount = iRA.IRABalance;
                    MaxContribution = MaxContribution - transaction.Amount;
                    transaction.Description = "Initial deposit into" + iRA.AccountNumber;


                    if(iRA.AccountBalance >5000)
                    {
                        transaction.TransactionStatus = TransactionStatus.Pending;
                        iRA.AccountBalance = 0;


                    }

                    else
                    {
                        transaction.TransactionStatus = TransactionStatus.Approved;
                    }

                    if (iRA.Product.Customer.Age > 65)
                    {
                        iRA.IRAStatus = true;
                        ViewBag.StatusMessage = "You are qualified to transfer money.";

                    }


                    iRA.IRAStatus = false;

                    _context.Transactions.Add(transaction);
                    iRA.Transaction = new List<Transaction>();
                    iRA.Transaction.Add(transaction);
                    _context.IRAs.Add(iRA);
                    _context.SaveChanges();
                    return RedirectToAction("Details", "Account");
                }
            }
            return View(iRA);
        }

        // GET: IRA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iRA = await _context.IRAs.FindAsync(id);
            if (iRA == null)
            {
                return NotFound();
            }
            return View(iRA);
        }

        // POST: IRA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IRAID,ContributionsThisYear,StandardAccountID,AccountType,AccountName,AccountBalance,Overdraft,AccountStatus")] IRA iRA)
        {
            if (id != iRA.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iRA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IRAExists(iRA.ProductID))
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
            return View(iRA);
        }

        // GET: IRA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iRA = await _context.IRAs
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (iRA == null)
            {
                return NotFound();
            }

            return View(iRA);
        }

        // POST: IRA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iRA = await _context.IRAs.FindAsync(id);
            _context.IRAs.Remove(iRA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IRAExists(int id)
        {
            return _context.IRAs.Any(e => e.IRAID == id);
        }
    }
}
