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
        public async Task<IActionResult> CreateDeposit([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus,AccountBalance,AccountName,Contribution,ProductType,Age")] Transaction transaction, int SelectedProduct)
        {
			transaction.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

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

			if (transaction.Product.ProductType == ProductTypes.IRA)
			{
                if (transaction.AppUser.Age < 70 && (transaction.Product.Contribution + transaction.Amount) < 5000.00m)
				{
					
					ViewBag.StatusUpdate = "You've successfully deposited " + transaction.Amount.ToString() + " into your account.";
					transaction.TransactionStatus = TransactionStatus.Approved;
					transaction.Product.Contribution = product.Contribution + transaction.Amount;
					transaction.Product.AccountBalance = product.AccountBalance + transaction.Amount;

					_context.Add(transaction);
					await _context.SaveChangesAsync();
					return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });
				}
                else
				{
					ViewBag.ErrorIRAMessage = "You are not of age or have reached your maximum contribution for the year.";
					return RedirectToAction("CreateDeposit", "Transaction");
				}
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
        public async Task<IActionResult> CreateWithdrawal([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus,AccountBalance,AccountName,ProductType,Age")] Transaction transaction, int SelectedProduct)
        {
			transaction.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

			Product product = _context.Products.Find(SelectedProduct);
			transaction.Product = product;

			if (transaction.Product.ProductType == ProductTypes.IRA)
			{
				if (transaction.AppUser.Age < 66 && transaction.Amount <= 3000)
				{
					transaction.TransactionType = TransactionTypes.Withdrawal;
					transaction.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
					transaction.Date = DateTime.Now;
					if (transaction.Description == null)
					{
						transaction.Description = "Withdrawal";
					}
					Transaction fee = new Transaction();
					fee.Amount = -30.00m;
					fee.Description = "An overdraft fee of $30 has been applied";
					fee.Date = transaction.Date;
					fee.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
					fee.TransactionType = TransactionTypes.Fee;
					fee.Product = transaction.Product;
					_context.Transactions.Add(fee);

					transaction.Product.AccountBalance += fee.Amount;

					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction.Amount.ToString() + " into your account.";
					transaction.TransactionStatus = TransactionStatus.Approved;
					transaction.Product.AccountBalance = product.AccountBalance - transaction.Amount;
					_context.Add(transaction);
					await _context.SaveChangesAsync();
					return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });
				}
				if (transaction.AppUser.Age < 66 && transaction.Amount > 3000)
				{
					ViewBag.ErrorMessage = "You are not allowed to withdraw over $3000!";
					return RedirectToAction("CreateWithdrawal", "Transaction");
				}
				else
				{
					transaction.TransactionType = TransactionTypes.Withdrawal;
					transaction.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
					transaction.Date = DateTime.Now;
					if (transaction.Description == null)
					{
						transaction.Description = "Withdrawal";
					}
					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction.Amount.ToString() + " into your account.";
					transaction.TransactionStatus = TransactionStatus.Approved;
					transaction.Product.AccountBalance = product.AccountBalance - transaction.Amount;

					_context.Add(transaction);
					await _context.SaveChangesAsync();
					return RedirectToAction("Details", "Transaction", new { id = transaction.TransactionID });

				}
			}
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
                fee.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
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

		// GET: Transaction/CreateTransfer
		public IActionResult CreateTransfer(int id)
		{
			Transaction ts = new Transaction();
			//finds user's accounts
			ViewBag.SelectAccount = GetUserProducts();

			return View(ts);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTransfer([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus,AccountBalance,AccountName,ProductType,Age")] Transaction transaction, int AccountFrom, int AccountTo)
		{
			transaction.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

			Transaction transaction1 = new Transaction();
			Product accountfrom = _context.Products.Find(AccountFrom);
			transaction1.Product = accountfrom;
			transaction1.Amount = transaction.Amount;
			transaction1.TransactionType = TransactionTypes.Transfer;
			transaction1.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);

			Transaction transaction2 = new Transaction();
			Product accountto = _context.Products.Find(AccountTo);
			transaction2.Product = accountto;
			transaction2.Amount = transaction.Amount;

			transaction1.Description = "Transfer to account " + transaction2.Product.AccNumber;
			transaction2.Description = "Transfer from account " + transaction1.Product.AccNumber;

            if (transaction1.Product.ProductType == ProductTypes.Portfolio)
			{
				if (transaction1.Amount > transaction1.Product.AvailableCash)
				{
					ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
					return RedirectToAction("CreateTransfer", "Transaction");
				}
				// 1. cannot overdraft -> error
				if (transaction1.Product.AccountBalance < 0.00m || (transaction1.Product.AccountBalance - transaction1.Amount < 0.00m))
				{
					ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
					return RedirectToAction("CreateTransfer", "Transaction");
				}

				//checks if the withdraw is > 5000, updates the status
				if (transaction1.Amount > 5000)
				{
					ViewBag.LargeWithdrawalMessage = "Your withdrawal is $5000 or larger. You need to wait on Manager Approval";
					transaction1.TransactionStatus = TransactionStatus.Pending;
				}
				// portfolio amount is under cash value 
				else
				{
					transaction1.Product.AccountBalance -= transaction1.Amount;
					transaction1.Product.AvailableCash -= transaction1.Amount;
					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction1.Amount.ToString() + " into your account.";
					transaction1.TransactionStatus = TransactionStatus.Approved;
					//transaction1.Product.AccountBalance = accountfrom.AccountBalance - transaction1.Amount;
				}
			}
			
			if (transaction1.Product.ProductType == ProductTypes.IRA)
			{
				if (transaction.AppUser.Age < 66 && transaction1.Amount <= 3000)
				{

					Transaction fee = new Transaction();
					fee.Amount = -30.00m;
					fee.Description = "An overdraft fee of $30 has been applied";
					fee.Date = transaction.Date;
					fee.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
					fee.TransactionType = TransactionTypes.Fee;
					fee.Product = transaction1.Product;
					_context.Transactions.Add(fee);

					transaction1.Product.AccountBalance += fee.Amount;

					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction1.Amount.ToString() + " into your account.";
					transaction1.TransactionStatus = TransactionStatus.Approved;
					transaction1.Product.AccountBalance = accountfrom.AccountBalance - transaction1.Amount;
					
				}
				if (transaction.AppUser.Age < 66 && transaction1.Amount > 3000)
				{
					ViewBag.ErrorMessage = "You are not allowed to withdraw over $3000!";
					return RedirectToAction("CreateTransfer", "Transaction");
				}
				// 1. cannot overdraft -> error
				if (transaction1.Product.AccountBalance < 0.00m || (transaction1.Product.AccountBalance - transaction1.Amount < 0.00m))
				{
					ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
					return RedirectToAction("CreateTransfer", "Transaction");
				}

				else
				{
					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction1.Amount.ToString() + " into your account.";
					transaction1.TransactionStatus = TransactionStatus.Approved;
					transaction1.Product.AccountBalance = accountfrom.AccountBalance - transaction1.Amount;
				}
			}

			if (transaction1.Product.ProductType == ProductTypes.Checking || transaction1.Product.ProductType == ProductTypes.Savings)
			{
				// 1. cannot overdraft -> error
				if (transaction1.Product.AccountBalance < 0.00m || (transaction1.Product.AccountBalance - transaction1.Amount < 0.00m))
				{
					ViewBag.OverdraftMessage = "Transaction violates overdraft rules";
					return RedirectToAction("CreateTransfer", "Transaction");
				}

				//checks if the withdraw is > 5000, updates the status
				if (transaction1.Amount > 5000)
				{
					ViewBag.LargeWithdrawalMessage = "Your withdrawal is $5000 or larger. You need to wait on Manager Approval";
					transaction1.TransactionStatus = TransactionStatus.Pending;
				}
				else
				{
					ViewBag.StatusUpdate = "You've successfully withdrawn " + transaction1.Amount.ToString() + " into your account.";
					transaction1.TransactionStatus = TransactionStatus.Approved;
					transaction1.Product.AccountBalance = accountfrom.AccountBalance - transaction1.Amount;
				}

				// invalid amount entered
				if (transaction1.Amount < 0)
				{
					return View(transaction1);

				}
			}

			
			// updates the values from user input
			transaction2.TransactionType = TransactionTypes.Transfer;
			transaction2.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);

            if (transaction2.Product.ProductType == ProductTypes.Portfolio)
			{
				if (transaction2.Amount > 5000)
				{
					ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";
					transaction2.TransactionStatus = TransactionStatus.Pending;
				}
				else
				{
					transaction2.Product.AccountBalance = accountto.AccountBalance + transaction2.Amount;
					transaction2.Product.AvailableCash = accountto.AvailableCash + transaction2.Amount;
					ViewBag.StatusUpdate = "You've successfully deposited " + transaction2.Amount.ToString() + " into your account.";
					transaction2.TransactionStatus = TransactionStatus.Approved;
				}
			}

			if (transaction2.Product.ProductType == ProductTypes.IRA)
			{
				if (transaction.AppUser.Age < 70 && (transaction2.Product.Contribution + transaction2.Amount) < 5000.00m)
				{

					ViewBag.StatusUpdate = "You've successfully deposited " + transaction2.Amount.ToString() + " into your account.";
					transaction2.TransactionStatus = TransactionStatus.Approved;
					transaction2.Product.Contribution = accountto.Contribution + transaction2.Amount;
					transaction2.Product.AccountBalance = accountto.AccountBalance + transaction2.Amount;
				}
				else
				{
					ViewBag.ErrorIRAMessage = "You are not of age or have reached your maximum contribution for the year.";
					return RedirectToAction("CreateDeposit", "Transaction");
				}
			}

			if (transaction2.Product.ProductType == ProductTypes.Checking || transaction2.Product.ProductType == ProductTypes.Savings)
			{
				//checks if the deposit is > 5000, updates the status
				if (transaction2.Amount > 5000)
				{
					ViewBag.LargeDepositMessage = "Your deposit is $5000 or larger. You need to wait on Manager Approval";
					transaction2.TransactionStatus = TransactionStatus.Pending;
				}
				// valid deposit
				if (ModelState.IsValid)
				{
					ViewBag.StatusUpdate = "You've successfully deposited " + transaction2.Amount.ToString() + " into your account.";
					transaction2.TransactionStatus = TransactionStatus.Approved;
					transaction2.Product.AccountBalance = accountto.AccountBalance + transaction2.Amount;
				}
			}

			_context.Add(transaction1);
			await _context.SaveChangesAsync();

			_context.Add(transaction2);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Transaction");
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
		}
	
        public SelectList GetUserProducts()
        {
			var query = from p in _context.Products
						select p;

			query = query.Where(p => p.Customer.UserName == User.Identity.Name);


			List<Product> accountList = query.ToList();
			IEnumerable<SelectListItem> selectList = from p in query
													 select new SelectListItem
													 {
														 Value = p.ProductID.ToString(),
														 Text = p.AccountName + ": " + p.AccountBalance.ToString()
													 };
			SelectList accountSelection = new SelectList(selectList, "Value", "Text");
			return accountSelection;
			
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
