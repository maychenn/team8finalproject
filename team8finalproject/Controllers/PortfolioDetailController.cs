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
        public async Task<IActionResult> Index(Int32 stockID)
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






        //GET: PortfolioDetail/Create
        public IActionResult Create(Int32 productID)
        {

            PortfolioDetail Pdt = new PortfolioDetail();
            Pdt.Product = _context.Products.Find(productID);
            ViewBag.AllStock = GetAllStocks();

            return View(Pdt);
        }

        //POST: PortfolioDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortfolioDetailID,NumShares,StockPrice,ExtendedPrice")] PortfolioDetail portfolioDetail, int SelectedStock)
        {
            //find selected stock
            Stock stock = _context.Stocks.Find(SelectedStock);
            portfolioDetail.Stock = stock;

            // find correct product (stock portfolio)
            Product product = _context.Products.Find(portfolioDetail.Product.ProductID);
            portfolioDetail.Product = product;


            // set stock price
            portfolioDetail.StockPrice = stock.Price;

            //set stock Price*Quantity
            portfolioDetail.ExtendedPrice = portfolioDetail.StockPrice * portfolioDetail.NumShares;

            if (ModelState.IsValid)
            {
                _context.Add(portfolioDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { id = portfolioDetail.Product.ProductID });
            }
            ViewBag.AllStocks = GetAllStocks();
            return View(portfolioDetail);
        }


        // GET: Transaction/CreateDeposit
        public IActionResult PurchaseStock(int id)
        {
            //finds user's accounts
            ViewBag.SelectAccount = GetUserProducts();

            return View();
        }



        //GET: PortfolioDetail/Edit/5
        public IActionResult Edit(int? id)
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

        //POST: PortfolioDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PortfolioDetailID,NumShares,StockPrice,ExtendedPrice")] PortfolioDetail portfolioDetail)
        {
            PortfolioDetail dbPD = _context.PortfolioDetails
                .Include(p => p.Stock)
                .Include(p => p.Product)
                .FirstOrDefault(p => p.Product.ProductID == id);
            // update num of shares
            dbPD.NumShares = portfolioDetail.NumShares;

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

        //POST: PortfolioDetail/Delete/5
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