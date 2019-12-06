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
using Microsoft.Extensions.DependencyInjection;

namespace team8finalproject.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        // GET: Transaction/Select
        public IActionResult Select()
        {
            return View();
        }

        // POST: Transaction/Select
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Select(String dummy)
        {
            return View();

        }

        // GET: Transaction/CreateDeposit
        public IActionResult CreateDeposit(int id)
        {
            //finds user's accounts
            ViewBag.SelectAccount = GetCSIRAProducts();

            return View();
        }

        // POST: Product/CreateDeposit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeposit([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus")] Transaction transaction, int SelectedProduct)
        {
            Transaction ts = new Transaction();

            //find the correct product
            var userProducts = _context.Products
                .Where(p => p.Customer.UserName == User.Identity.Name)
                .Where(p => p.ProductType == ProductTypes.Checking || p.ProductType == ProductTypes.Savings || p.ProductType == ProductTypes.IRA)
                .OrderBy(x => x.ProductID).ToList();

            Product product = userProducts[SelectedProduct];

            ts.Product = product;

            // updates the values from user input
            ts.TransactionType = TransactionTypes.Deposit;
            ts.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
            ts.Date = DateTime.Now;
            if (transaction.Description != null)
            {
                ts.Description = transaction.Description;
            }

            if (transaction.Amount < 0)
            {
                ViewBag.SelectAccount = GetCSIRAProducts();
                return View(product);

            }
            ts.Amount = transaction.Amount;

            //checks if the deposit is > 5000, updates the status
            if (transaction.Amount > 5000)
            {
                ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";
                ts.TransactionStatus = TransactionStatus.Pending;
            }
            // valid deposit
            else
            {
                ViewBag.StatusUpdate = "You've successfully deposited " + ts.Amount.ToString() + " into your account.";
                ts.TransactionStatus = TransactionStatus.Approved;
                ts.Product.AccountBalance = product.AccountBalance + ts.Amount;
            }



            _context.Add(ts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Transaction", new { id = ts.TransactionID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(ts);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Transaction", new { id = ts.TransactionID });
            }*/

        }

        // GET: Transaction/CreateWithdrawal
        public IActionResult CreateWithdrawal(int id)
        {
            //finds user's accounts
            ViewBag.SelectAccount = GetCSIRAProducts();

            return View();
        }

        // POST: Product/CreateWithdrawal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithdrawal([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus")] Transaction transaction, int SelectedProduct)
        {
            Transaction ts = new Transaction();

            //find the correct product
            var userProducts = _context.Products
                .Where(p => p.Customer.UserName == User.Identity.Name)
                .Where(p => p.ProductType == ProductTypes.Checking || p.ProductType == ProductTypes.Savings || p.ProductType == ProductTypes.IRA)
                .OrderBy(x => x.ProductID).ToList();
            Product product = userProducts[SelectedProduct];
            ts.Product = product;

            // 1. cannot overdraft -> error
            if (product.AccountBalance < 0 || (product.AccountBalance - transaction.Amount < -50))
            {
                ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
                return View(transaction);
            }
            // 2. can overdraft -> $30 overdraft fee
            if ((product.AccountBalance - transaction.Amount) < 0)
            {
                Transaction fee = new Transaction();
                fee.Amount = -30.00m;
                fee.Description = "An overdraft fee of $30 has been applied";
                fee.Date = transaction.Date;
                fee.Number = transaction.Number;
                fee.TransactionType = TransactionTypes.Fee;
                fee.Product = transaction.Product;
                _context.Transactions.Add(fee);

                product.AccountBalance += fee.Amount;
            }

            // updates the values from user input
            ts.TransactionType = TransactionTypes.Withdrawal;
            ts.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
            ts.Date = DateTime.Now;
            ts.Amount = transaction.Amount;
            if (transaction.Description != null)
            {
                ts.Description = transaction.Description;
            }

            //checks if the deposit is > 5000, updates the status
            if (transaction.Amount > 5000)
            {
                ViewBag.LargeWithdrawalMessage = "Your withdrawal is $5000 or larger. You need to wait on Manager Approval";
                ts.TransactionStatus = TransactionStatus.Pending;
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully withdrawn " + ts.Amount.ToString() + " into your account.";
                ts.TransactionStatus = TransactionStatus.Approved;
                ts.Product.AccountBalance = product.AccountBalance - transaction.Amount;
            }

            // invalid amount entered
            if (transaction.Amount < 0)
            {
                return View(product);

            }

            // saves the transaction
            _context.Entry(product).State = EntityState.Modified;
            _context.Add(ts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Transaction", new { id = ts.TransactionID });
            /*
            if (ModelState.IsValid)
            {
                _context.Add(ts);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Transaction", new { id = ts.TransactionID });
            }*/

        }




        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus")] Transaction transaction, int SelectedProduct, int SelectedTransaction)
        {

            //Find the Selected Transaction
            //Transaction trans = _context.Transactions.Find(SelectedTransaction);
            //trans.Transactions = trans;

            //Find the selected Account
            Product product = _context.Products.Find(SelectedProduct);
            transaction.Product = product;


            transaction.TransactionType = TransactionTypes.Deposit;

            transaction.Product.AccountBalance = transaction.Product.AccountBalance + transaction.Amount;

            if (transaction.Amount <= 0)
            {
                ViewBag.Error = "Amount must be greater than $0.00.";
                return View(transaction);
            }


            if (transaction.Amount >= 5000)
            {

                transaction.TransactionStatus = TransactionStatus.Pending;
                ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";

            }

            else
            {

                product.AccountBalance = product.AccountBalance + transaction.Amount;

            }

            Transaction deposit = new Transaction();
            deposit.Description = "New Desposit";
            deposit.Date = transaction.Date;
            deposit.Number = transaction.Number;
            deposit.TransactionType = TransactionTypes.Deposit;
            deposit.Product = transaction.Product;
            _context.Transactions.Add(deposit);
            transaction.Product.AccountBalance += deposit.Amount;
            _context.Transactions.Add(transaction);
            _context.SaveChangesAsync();
            ViewBag.Message = "Successful Deposit";

            return RedirectToAction("Index", "Transaction", new { id = transaction.TransactionID });



            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Transaction", new { id = transaction.TransactionID });

            }
            return View(transaction);
        }


        //Get: Deposit
        public ActionResult Deposit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST: Deposit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind("TransactionID,Date,TransactionType,Description,Amount,TransactionStatus")] Transaction transaction, int SelectedAccount)
        {
            if (ModelState.IsValid)
            {

                Product product = _context.Products.Find(SelectedAccount);



                ViewBag.AllProducts = GetAllProducts();
                if (transaction.Amount <= 0)
                {
                    ViewBag.Error = "Amount must be greater than $0.00.";
                    return View(transaction);
                }


                if (transaction.Amount >= 5000)
                {

                    transaction.TransactionStatus = TransactionStatus.Pending;
                    ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";

                }

                else
                {

                    product.AccountBalance = product.AccountBalance + transaction.Amount;

                }
            }
            //string LastFour = transaction.Product.AccountNumber.ToString().Substring(transaction.Product.AccountNumber.Length - 4, 4);
            //ViewBag.Last4Digits = "XXXXXX" + LastFour.ToString();

            return View(transaction);
        }

        [HttpGet]
        public ActionResult Withdrawal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.String = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdrawal([Bind("TransactionID,Number,Date,TransactionType,Description,Amount,TransactionStatus")] Transaction transaction, Int32 ProductID)
        {
            if (ModelState.IsValid)
            {
                if (transaction.Amount == 0)
                {
                    ViewBag.Error = "Withdrawal amount cannot be $0!";
                    return View(transaction);
                }
                if (transaction.Amount > 0)
                {
                    //transaction.Amount = -transaction.Amount;
                }
                Product product = _context.Products.Find(ProductID);
                transaction.Product = product;
                transaction.TransactionType = TransactionTypes.Withdrawal;
                product.AccountBalance = product.AccountBalance - transaction.Amount;
                if (product.AccountBalance < 0)
                {
                    if (product.AccountBalance < -50)
                    {
                        ViewBag.Message = "Transaction exceeds $50 overdraft limit";
                        product.AccountBalance -= transaction.Amount;
                        ModelState.Remove("Amount");
                        transaction.Amount = 50 + product.AccountBalance;
                        return View(transaction);
                    }
                    Transaction fee = new Transaction();
                    fee.Amount = -30;
                    fee.Description = "Overdraft fee";
                    fee.Date = transaction.Date;
                    fee.Number = transaction.Number;
                    fee.TransactionType = TransactionTypes.Fee;
                    fee.Product = transaction.Product;
                    _context.Transactions.Add(fee);
                    product.AccountBalance += fee.Amount;
                }
                _context.Entry(product).State = EntityState.Modified;
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = transaction.Product.ProductID });
            }
            return View(transaction);
        }


        private SelectList GetAllProducts()
        {
            //get a list of all courses from the database
            List<Product> AllProducts = _context.Products.ToList();

            //convert this to a select list
            //note that ProductID and Name are the names of fields in the Product model class
            SelectList products = new SelectList(AllProducts, "ProductID", "ProductType");

            //return the select list
            return products;

        }

        public SelectList GetCSIRAProducts()
        {
            //get a list of all Checking, Savings, and IRA accounts from the database
            List<Product> AllProducts = _context.Products
                .Where(p => p.Customer.UserName == User.Identity.Name)
                .Where(p => p.ProductType == ProductTypes.Checking || p.ProductType == ProductTypes.Savings || p.ProductType == ProductTypes.IRA)
                .OrderBy(x => x.ProductID).ToList();


            //convert this to a select list
            SelectList products = new SelectList(AllProducts, "", "AccountName");

            //return the select list
            return products;
		}
        public SelectList GetUserProducts()
        {
            //get a list of all of the user's accounts from the database
            List<Product> AllProducts = _context.Products
                .Where(p => p.Customer.UserName == User.Identity.Name)
                .OrderBy(x => x.ProductID).ToList();

            //convert this to a select list
            SelectList products = new SelectList(AllProducts, "ProductID", "AccountName");

            //return the select list
            return products;
        }




    }

}








        //// GET: Transaction/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

//    var transaction = await _context.Transactions.FindAsync(id);
//    if (transaction == null)
//    {
//        return NotFound();
//    }





//    return View(transaction);
//}



//        // POST: Transaction/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus")] Transaction transaction)
//        {
//            if (id != transaction.TransactionID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(transaction);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!TransactionExists(transaction.TransactionID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(transaction);
//        }

//        // GET: Transaction/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var transaction = await _context.Transactions
//                .FirstOrDefaultAsync(m => m.TransactionID == id);
//            if (transaction == null)
//            {
//                return NotFound();
//            }

//            return View(transaction);
//        }

//        // POST: Transaction/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var transaction = await _context.Transactions.FindAsync(id);
//            _context.Transactions.Remove(transaction);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool TransactionExists(int id)
//        {
//            return _context.Transactions.Any(e => e.TransactionID == id);
//        }
//    }
//}
