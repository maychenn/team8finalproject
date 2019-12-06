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

        //GET: PortfolioDetail
        public IActionResult Index(Int32 stockID)
        {
            List<PortfolioDetail> Pdt = _context.PortfolioDetails
                .Include(Pd => Pd.Stock)
                .Include(pd => pd.Product.Customer)
                .Where(P => P.Product.ProductID == stockID).ToList();
            return View(Pdt);

        }
        /*
        //GET: PortfolioDetail/Details/5
        public async Task<IActionResult> Details(int? id)
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

            foreach (var s in _context.Stocks)
            {
                s.Price = s.Price;
            }
            foreach (var p in _context.Transactions)
            {
                p. = p.Stock.CurrentPrice * p.NumberOfShares;
            }
            if (stockPortfolio.StockPurchases.Count != 0)
            {
                stockPortfolio.StockPortionValue = 0;
                foreach (var v in stockPortfolio.StockPurchases)
                {
                    stockPortfolio.StockPortionValue += v.TotalStockValue;
                    v.ChangeInPrice = v.Stock.CurrentPrice - v.InitialSharePrice;
                    v.TotalChange = v.TotalStockValue - (v.NumberOfShares * v.InitialSharePrice);
                    v.StockPurchaseDisplay = v.Stock.StockName + ", Current Price: " + v.Stock.CurrentPrice.ToString("c") + ", Number of shares: " + v.NumberOfShares;
                }
            }
            db.SaveChanges();

            return View(portfolioDetail);
        }
        */


        //GET: PortfolioDetail/Purchase
        public IActionResult Purchase(Int32 stockID)
        {
            // finds the user's portfolio
            PortfolioDetail Pdt = new PortfolioDetail();
            var product = _context.Products.Where(p => p.Customer.UserName == User.Identity.Name)
                .Where(p => p.ProductType == ProductTypes.Portfolio).ToList();
            Pdt.Product = product[0];
            // finds the stock
            Pdt.Stock = _context.Stocks.Find(stockID);
            return View(Pdt);
        }

        //POST: PortfolioDetail/Purchase
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase([Bind("PortfolioDetailID,NumShares,StockPrice,ExtendedPrice,Balanced")] PortfolioDetail portfolioDetail)
        {
            // find correct product (stock portfolio)
            Product product = _context.Products.Find(portfolioDetail.Product.ProductID);
            portfolioDetail.Product = product;

            // set stock price
            portfolioDetail.StockPrice = portfolioDetail.Stock.Price;

            // set extended price
            portfolioDetail.ExtendedPrice = portfolioDetail.StockPrice * portfolioDetail.NumShares;

            // update Stock Value
            portfolioDetail.Product.StockValue += portfolioDetail.ExtendedPrice;

            if (ModelState.IsValid)
            {
                _context.Add(portfolioDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { id = portfolioDetail.Product.ProductID });
            }
            ViewBag.AllStocks = GetAllStocks();
            return View(portfolioDetail);
        }

        //GET: PortfolioDetail/Sell/5
        public IActionResult Sell(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PortfolioDetail portfolioDetail = _context.PortfolioDetails
                .Include(p => p.Stock)
                .Include(p => p.Product)
                .FirstOrDefault(p => p.Product.ProductID == id);
            
            if (portfolioDetail == null)
            {
                return NotFound();
            }
            return View(portfolioDetail);
        }

        //POST: PortfolioDetail/Sell/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sell(int id, [Bind("PortfolioDetailID,NumShares,StockPrice,ExtendedPrice")] PortfolioDetail portfolioDetail)
        {
            PortfolioDetail dbPD = _context.PortfolioDetails
                .Include(p => p.Stock)
                .Include(p => p.Product)
                .FirstOrDefault(p => p.Product.ProductID == id);

            // update num of shares
            dbPD.NumShares = portfolioDetail.NumShares;

            // set extended price
            portfolioDetail.ExtendedPrice = portfolioDetail.StockPrice * portfolioDetail.NumShares;

            // update Stock Value
            portfolioDetail.Product.StockValue -= portfolioDetail.ExtendedPrice;

            if (id != portfolioDetail.PortfolioDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(dbPD);
                _context.SaveChanges();
                RedirectToAction("Details", "Products", new { id = dbPD.Product.ProductID });
            }
            return View(dbPD);
        }
        /*
        //GET: PortfolioDetail/Delete/5
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

        //POST: PortfolioDetail/Sell/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioDetail = await _context.PortfolioDetails.FindAsync(id);
            _context.PortfolioDetails.Remove(portfolioDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool PortfolioDetailExists(int id)
        {
            return _context.PortfolioDetails.Any(e => e.PortfolioDetailID == id);
        }

        public SelectList GetUserProducts()
        {
            //get a list of all products from the database
            List<Product> AllProducts = _context.Products.Where(p => p.Customer.UserName == User.Identity.Name)
                .OrderBy(x => x.ProductID).ToList();


            //convert this to a select list
            //note that ProductID and ProductName are the names of fields in the Product model class
            SelectList products = new SelectList(AllProducts, "ProductID", "AccountName");

            //return the select list
            return products;

        }




         private SelectList GetAllStocks()

        {
            //get a list of all stocks from the database
            List<Stock> AllStocks = _context.Stocks.ToList();

            //convert this to a select list
            SelectList stocks = new SelectList(AllStocks, "StockID", "StockName");

            //return the select list
            return stocks;

        }

    }
}