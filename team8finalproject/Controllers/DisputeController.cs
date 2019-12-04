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
    public class DisputeController : Controller
    {
        private readonly AppDbContext _context;

        public DisputeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dispute
        public async Task<IActionResult> Index()
        {
            return View(await _context.Disputes.ToListAsync());
        }

        // GET: Dispute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputeID == id);
            if (dispute == null)
            {
                return NotFound();
            }

            return View(dispute);
        }

        // GET: Dispute/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dispute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisputeID,NewAmount,Description,ManagerComment,DisputeStatus")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispute);
        }

        // GET: Dispute/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes.FindAsync(id);
            if (dispute == null)
            {
                return NotFound();
            }
            return View(dispute);
        }

        // POST: Dispute/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisputeID,NewAmount,Description,ManagerComment,DisputeStatus")] Dispute dispute)
        {
            if (id != dispute.DisputeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisputeExists(dispute.DisputeID))
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
            return View(dispute);
        }

        // GET: Dispute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispute = await _context.Disputes
                .FirstOrDefaultAsync(m => m.DisputeID == id);
            if (dispute == null)
            {
                return NotFound();
            }

            return View(dispute);
        }

        // POST: Dispute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dispute = await _context.Disputes.FindAsync(id);
            _context.Disputes.Remove(dispute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisputeExists(int id)
        {
            return _context.Disputes.Any(e => e.DisputeID == id);
        }
    }
}
