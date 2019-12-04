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
    public class PortfolioDetailController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioDetailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PortfolioDetail
        //public async Task<IActionResult> Index(Int32 stockID)
        //{
        //    List<PortfolioDetail> Pdt = _context.PortfolioDetails
        //        .Include(Pd =>Pd.Stock)
        //        .Where(P => P.Product.Transaction. == stockID).ToList();
        //    return View(Pdt);

            //return View(await _context.PortfolioDetails.ToListAsync());
        }

        // GET: PortfolioDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var portfolioDetail = await _context.PortfolioDetails
            //    .FirstOrDefaultAsync(m => m.PortfolioDetailID == id);
            //if (portfolioDetail == null)
            //{
            //    return NotFound();
            //}

            //foreach (var s in _context.Stocks)
            //{
            //    s.Price = s.Price;
            //}
            //foreach (var p in _context.Transactions)
            //{
            //    p. = p.Stock.CurrentPrice * p.NumberOfShares;
            //}
            //if (stockPortfolio.StockPurchases.Count != 0)
            //{
            //    stockPortfolio.StockPortionValue = 0;
            //    foreach (var v in stockPortfolio.StockPurchases)
            //    {
            //        stockPortfolio.StockPortionValue += v.TotalStockValue;
            //        v.ChangeInPrice = v.Stock.CurrentPrice - v.InitialSharePrice;
            //        v.TotalChange = v.TotalStockValue - (v.NumberOfShares * v.InitialSharePrice);
            //        v.StockPurchaseDisplay = v.Stock.StockName + ", Current Price: " + v.Stock.CurrentPrice.ToString("c") + ", Number of shares: " + v.NumberOfShares;
            //    }
            //}
            //db.SaveChanges();














            return View(portfolioDetail);
        }

        // GET: PortfolioDetail/Create
        public IActionResult Create(Int32 stockID)
        {

            PortfolioDetail Pdt = new PortfolioDetail();
            Pdt.




            return View();
        }

        // POST: PortfolioDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortfolioDetailID,NumShares,StockPrice")] PortfolioDetail portfolioDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioDetail);
        }

        // GET: PortfolioDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioDetail = await _context.PortfolioDetails.FindAsync(id);
            if (portfolioDetail == null)
            {
                return NotFound();
            }
            return View(portfolioDetail);
        }

        // POST: PortfolioDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PortfolioDetailID,NumShares,StockPrice")] PortfolioDetail portfolioDetail)
        {
            if (id != portfolioDetail.PortfolioDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioDetailExists(portfolioDetail.PortfolioDetailID))
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
            return View(portfolioDetail);
        }

        // GET: PortfolioDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioDetail = await _context.PortfolioDetails
                .FirstOrDefaultAsync(m => m.PortfolioDetailID == id);
            if (portfolioDetail == null)
            {
                return NotFound();
            }

            return View(portfolioDetail);
        }

        // POST: PortfolioDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioDetail = await _context.PortfolioDetails.FindAsync(id);
            _context.PortfolioDetails.Remove(portfolioDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioDetailExists(int id)
        {
            return _context.PortfolioDetails.Any(e => e.PortfolioDetailID == id);
        }
    }
}
