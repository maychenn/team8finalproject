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
			Transaction ts = new Transaction();
			//finds user's accounts
			ViewBag.SelectAccount = GetCSIRAProducts();

            return View(ts);
        }

        // POST: Product/CreateDeposit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeposit([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus,AccountBalance,AccountName")] Transaction transaction, int SelectedProduct)
        {

			Product product = _context.Products.Find(SelectedProduct);
			transaction.Product = product;

            // updates the values from user input
            transaction.TransactionType = TransactionTypes.Deposit;
            transaction.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
            transaction.Date = DateTime.Now;
            if (transaction.Description == null)
            {
                transaction.Description = "Deposit";
            }

			//checks if the deposit is > 5000, updates the status
			if (transaction.Amount > 5000)
			{
				ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";
				transaction.TransactionStatus = TransactionStatus.Pending;
			}
				// valid deposit
			if (ModelState.IsValid)
			{
				ViewBag.StatusUpdate = "You've successfully deposited " + transaction.Amount.ToString() + " into your account.";
				transaction.TransactionStatus = TransactionStatus.Approved;
				transaction.Product.AccountBalance = product.AccountBalance + transaction.Amount;
			}
			_context.Add(transaction);
			await _context.SaveChangesAsync();
			return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });
			

        }

        // GET: Transaction/CreateWithdrawal
        public IActionResult CreateWithdrawal(int id)
        {
			Transaction ts = new Transaction();
			//finds user's accounts
			ViewBag.SelectAccount = GetCSIRAProducts();

            return View(ts);
        }

        // POST: Product/CreateWithdrawal
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithdrawal([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus,AccountBalance,AccountName")] Transaction transaction, int SelectedProduct)
        {
			Product product = _context.Products.Find(SelectedProduct);
			transaction.Product = product;

			// updates the values from user input
			transaction.TransactionType = TransactionTypes.Withdrawal;
			transaction.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
			transaction.Date = DateTime.Now;
			if (transaction.Description == null)
			{
				transaction.Description = "Withdraw";
			}


			// 1. cannot overdraft -> error
			if (transaction.Product.AccountBalance < 0.00m || (transaction.Product.AccountBalance - transaction.Amount < -50.00m))
            {
                ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
                return View(transaction);
            }
            // 2. can overdraft -> $30 overdraft fee
            if ((transaction.Product.AccountBalance - transaction.Amount) < 0)
            {
                Transaction fee = new Transaction();
                fee.Amount = -30.00m;
                fee.Description = "An overdraft fee of $30 has been applied";
                fee.Date = transaction.Date;
                fee.Number = Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
				fee.TransactionType = TransactionTypes.Fee;
                fee.Product = transaction.Product;
                _context.Transactions.Add(fee);

                transaction.Product.AccountBalance += fee.Amount;
            }

            //checks if the withdraw is > 5000, updates the status
            if (transaction.Amount > 5000)
            {
                ViewBag.LargeWithdrawalMessage = "Your withdrawal is $5000 or larger. You need to wait on Manager Approval";
                transaction.TransactionStatus = TransactionStatus.Pending;
            }
            else
            {
                ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction.Amount.ToString() + " into your account.";
                transaction.TransactionStatus = TransactionStatus.Approved;
                transaction.Product.AccountBalance = product.AccountBalance - transaction.Amount;
            }

            // invalid amount entered
            if (transaction.Amount < 0)
            {
                return View(transaction);

            }

            // saves the transaction
            _context.Entry(product).State = EntityState.Modified;
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });

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
			var query = from p in _context.Products
						select p;
			
			query = query.Where(p => p.Customer.UserName == User.Identity.Name && (p.ProductType == ProductTypes.Checking || p.ProductType == ProductTypes.Savings || p.ProductType == ProductTypes.IRA));
			

			List<Product> accountList = query.ToList();
			IEnumerable<SelectListItem> selectList = from p in query
													 select new SelectListItem
													 {
														 Value = p.ProductID.ToString(),
														 Text = p.AccountName + ": " + p.AccountBalance.ToString()
													 };
			SelectList accountSelection = new SelectList(selectList, "Value", "Text");
			return accountSelection;
			/*SelectList accountSelection = new SelectList(accountList, "ProductID", "Text");
            return accountSelection;*/
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
