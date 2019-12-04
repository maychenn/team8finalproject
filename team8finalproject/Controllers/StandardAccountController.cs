using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using team8finalproject.DAL;
using team8finalproject.Models;

namespace team8finalproject.Controllers
{
    public class StandardAccountController : Controller
    {
        private readonly AppDbContext _context;

        public StandardAccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StandardAccount
        public async Task<IActionResult> Index()
        {
            return View(await _context.StandardAccounts.ToListAsync());
        }

        // GET: StandardAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccounts
                .FirstOrDefaultAsync(m => m.StandardAccountID == id);
            if (standardAccount == null)
            {
                return NotFound();
            }

            return View(standardAccount);
        }

        // GET: StandardAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StandardAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StandardAccountID,AccountType,AccountName,AccountBalance,Overdraft,AccountStatus")] StandardAccount standardAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standardAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(standardAccount);
        }

        // GET: StandardAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccounts.FindAsync(id);
            if (standardAccount == null)
            {
                return NotFound();
            }
            return View(standardAccount);
        }

        // POST: StandardAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StandardAccountID,AccountType,AccountName,AccountBalance,Overdraft,AccountStatus")] StandardAccount standardAccount)
        {
            if (id != standardAccount.StandardAccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standardAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardAccountExists(standardAccount.StandardAccountID))
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
            return View(standardAccount);
        }

        // GET: StandardAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardAccount = await _context.StandardAccounts
                .FirstOrDefaultAsync(m => m.StandardAccountID == id);
            if (standardAccount == null)
            {
                return NotFound();
            }

            return View(standardAccount);
        }

        // POST: StandardAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standardAccount = await _context.StandardAccounts.FindAsync(id);
            _context.StandardAccounts.Remove(standardAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardAccountExists(int id)
        {
            return _context.StandardAccounts.Any(e => e.StandardAccountID == id);
        }
    }
}
