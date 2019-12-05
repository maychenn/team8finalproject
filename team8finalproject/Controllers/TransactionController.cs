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
    public class TransactionController : Controller
    {
        private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;

		public TransactionController(AppDbContext context)
        {
            _context = context;
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

        // GET: Transaction/Create
        public IActionResult Create()
        {
			Viewbag.SelectType = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>();
			ViewBag.AllProducts = GetAllProducts();
			ViewBag.Accounts = FindAccounts();
			return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionID,Number,Description,Date,Amount,TransactionType,TransactionStatus")] Transaction transaction, int SelectedProduct, int SelectedTransaction)
        {

			//Find the Selected Transaction
			Transaction transaction = _context.Transactions.Find(SelectedTransaction);
			transaction.Transactions = transaction;

			////Find the selected Account
			Product product = _context.Products.Find(SelectedProduct);
			transaction.Product = product;


			transaction.TransactionType = TransactionTypes.Deposit;

			transaction.Product.Balance = transaction.Product.Balance + transaction.Amount;

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
			deposite.Description = "New Desposit";
			deposit.Date = transaction.Date;
			deposit.Number = transaction.Number;
			deposit.TransactionType = TransactionTypes.Deposit;
			deposit.Product = transaction.Product;
			_context.Transactions.Add(deposit);
			transaction.Product.AccountBalance += deposit.Amount;
			_context.Transactions.Add(transaction);
			_context.SaveChangesAsync();
			ViewBag.Message = "Successful Deposit";

			return RedirectToAction("Index", "Transaction", new { id = transaction.TransactionID});
		


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
        public ActionResult Deposit([Bind("TransactionID,Date,TransactionType,Description,Amount,TransactionStatus")] Transaction transaction)
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

		public SelectList FindAccounts()
		{
			var query = from p in _context.Products
						select p;
			{
				query = query.Where(p => p.ProductID == ProductID);
			}

			List<Product> accountList = query.ToList();
			IEnumerable<SelectListItem> selectList = from p in query
													 select new SelectListItem
													 {
														 Value = p.ProductID.ToString(),
														 Text = p.AccountName + " -- " + p.AccountBalance.ToString()
													 };
			SelectList accountSelection = new SelectList(selectList, "Value", "Text");
			return accountSelection;
			/*SelectList accountSelection = new SelectList(accountList, "ProductID", "Text");
            return accountSelection;*/
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
