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

namespace team8finalproject.Controllers
{
    public class DisputeController : Controller
    {
        private readonly AppDbContext _context;

        public DisputeController(AppDbContext context)
        {
            _context = context;
        }


        // GET: All Disputes (admin), all disputes for that customer
        [Authorize]
        public IActionResult Index()
        {
            List<Dispute> disputes = new List<Dispute>();
            if (User.IsInRole("Admin"))
            {
                // shows unresolved disputes
                disputes = _context.Disputes.Include(r => r.Transaction)
                    .Where(ds => ds.DisputeStatus == DisputeStatus.Submitted).ToList();

            }
            else //user is customer
            {
                disputes = _context.Disputes.Where(r => r.Transaction.AppUser.UserName == User.Identity.Name).Include(r => r.Transaction).ToList();
            }
            return View(disputes);
        }

        [Authorize]
        // GET: Dispute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // finds the dispute
            var dispute = await _context.Disputes
                .Include(t => t.Transaction).ThenInclude(t=>t.Product)
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
            Dispute dispute = new Dispute();
            dispute.Delete = false;
            dispute.DisputeStatus = DisputeStatus.Submitted;
            return View(dispute);
        }

        // POST: Dispute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,DisputeID,NewAmount,Description,Delete,DisputeStatus")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {

                //dispute.Transaction.TransactionStatus = dispute.Transaction.TransactionStatus.Pending;
                _context.Add(dispute);
          
                await _context.SaveChangesAsync();
				// redirects to the transaction/detail of the dispute just created
				return RedirectToAction("Details", "Transaction", new { transactionID = dispute.Transaction.TransactionID });
			}
            return View(dispute);
        }

        // GET: Dispute/Edit/5
        // only managers can change dispute (status)
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Dispute dispute = _context.Disputes
                .Include(r => r.Transaction).ThenInclude(r => r.AppUser)
                //.Include(r => r.Transaction)
                .FirstOrDefault(reg => reg.DisputeID == id);

            //dispute = _context.Disputes.Include(o => o.Transaction).ToList();

            if (dispute == null)
            {
                return View("Error", new String[] { "Cannot find the dispute you wish to edit!" });
            }
            return View(dispute);
        }

        // POST: Dispute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("DisputeID,NewAmount,Description,ManagerComment,DisputeStatus")] Dispute dispute, Decimal? adjustedAmount)
        {
            Dispute dp = _context.Disputes
                .Include(t => t.Transaction).ThenInclude(r => r.AppUser)
                .FirstOrDefault(d => dispute.DisputeID == id);

            // dispute not found
            if (id != dispute.DisputeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // manager accepts
                if (dispute.DisputeStatus == DisputeStatus.Accepted)
                {
                    dp.Transaction.Amount = dispute.NewAmount;
                    EmailMessaging.SendEmail(dp.Transaction.AppUser.Email, "Dispute Accepted", "Your Dispute has been Accepted");

                }
                // rejects
                else if (dispute.DisputeStatus == DisputeStatus.Rejected)
                {
                    EmailMessaging.SendEmail(dp.Transaction.AppUser.Email, "Dispute Rejected", "Your Dispute has been rejected");

                }
                // adjusts
                else if (dispute.DisputeStatus == DisputeStatus.Adjusted & adjustedAmount != null)
                {
                    dp.Transaction.Amount = (decimal)adjustedAmount;
                    EmailMessaging.SendEmail(dp.Transaction.AppUser.Email, "Transaction Adjusted", "Your Transaction has been Adjusted");
                }

                try
                {
                    _context.Update(dp);
                    _context.SaveChanges();
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
                // brings them back to all disputes
                return RedirectToAction("Index", "Dispute", new { transactionID = dispute.Transaction.TransactionID });
            }
            return View(dispute);
        }
        /* CANNOT DELETE DISPUTE
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
        */

        private bool DisputeExists(int id)
        {
            return _context.Disputes.Any(e => e.DisputeID == id);
        }
    }
}
