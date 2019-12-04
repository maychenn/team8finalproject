using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using team8finalproject.Models;
using team8finalproject.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using team8finalproject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace team8finalproject.Controllers
{
    public class TransactionSearchController : Controller
    {
        //Create an instance of the db_context
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        //Create the constructor so that we get an instance of AppDbContext
        public TransactionSearchController(AppDbContext context, IServiceProvider service)
        {
            _context = context;
            _userManager = service.GetRequiredService<UserManager<AppUser>>();
        }

        // GET: Transactions
        public IActionResult Index()
        {
            List<Transaction> transactions = new List<Transaction>();
            if (User.IsInRole("Admin"))
            {
                transactions = _context.Transactions.Include(r => r.Dispute).ToList();
            }
            else //user is customer
            {
                transactions = _context.Transactions.Where(r => r.Account.Customer.UserName == User.Identity.Name).Include(r => r.Dispute).ToList();
            }

            return View(transactions);
        }
        // quick search??
        public IActionResult Index(string SearchString)
        {
            var query = from t in _context.Transactions
                        select t;


            if (SearchString != null && SearchString != "")

            {
                query = query.Where(t => t.Description.Contains(SearchString));

            }

            List<Transaction> SelectedTransactions = query.Include(t => t.Account).ToList();

            ViewBag.AllTransactionCount = _context.Transactions.Count();
            ViewBag.SelectedTransactionCount = SelectedTransactions.Count();

            return View(SelectedTransactions.OrderByDescending(t => t.Number));

        }

        // GET: Transaction/Details
        public IActionResult Details(int? id)
        {
            if (id == null) //transaction ID not specified
            {
                return View("Error", new String[] { "TransactionID not specified - which transaction do you want to view?" });
            }

            Transaction transaction = _context.Transactions.Include(t => t.Number).FirstOrDefault(t => t.TransactionID == id);

            if (transaction == null) //Book does not exist in database
            {
                return View("Error", new String[] { "Transaction not found in database" });
            }

            //if code gets this far, all is well
            return View(transaction);
        }
        // GET: Transaction/Create
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([Bind("TransactionID, Amount, TransactionType")] Transaction transaction)
        {
            // gets the order number and date
            transaction.Number = (int)Utilities.GenerateTransactionNumber.GetNextTransactionNumber(_context);
            transaction.Date = DateTime.Now;

            transaction.Account.Customer = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Transaction", new { TransactionID = transaction.TransactionID });
            }
            return View(transaction);
        }

        // GET: Detailed Search
        public IActionResult DetailedSearch()
        {
            ViewBag.AllAccounts = GetAllAccounts();

            //default properties
            SearchViewModel svm = new SearchViewModel();
            svm.SelectedAccountID = 0;    //SelectedAccount is an ID number

            return View("DetailedSearch");
        }

        // GET: All Acounts
        private SelectList GetAllAccounts()
        {
            List<StandardAccount> AccountList = _context.StandardAccounts.ToList();

            StandardAccount SelectNone = new StandardAccount() { StandardAccountID = 0, AccountName = "All Accounts" }; AccountList.Add(SelectNone);


            SelectList AccountSelectList = new SelectList(AccountList.OrderBy(m => m.StandardAccountID), "StandardAccountID", "AccountName");

            return AccountSelectList;
        }
        // should be employees & managers only
        public IActionResult DisplaySearchResults(SearchViewModel svm)
        {
            // gets all transactions
            var query = from t in _context.Transactions
                        select t;

            // search by transaction number
            if (svm.TransactionNumber != null && svm.TransactionNumber != "")
            {

                query = query.Where(t => t.Number.Equals(svm.TransactionNumber));

            }
            // description
            if (svm.TransactionDescription != null && svm.TransactionDescription != "")

            {
                query = query.Where(t => t.Description.Contains(svm.TransactionDescription));

            }
            // StandardStandardStandardStandardStandardAccountID
            if (svm.SelectedAccountID != 0)

            {
                query = query.Where(t => t.Account.StandardAccountID == svm.SelectedAccountID);
            }
            // transaction amount (range)
            if (svm.AmountRange != null)
            {
                // low
                if (svm.AmountRange == AmountRanges.Low)
                {
                    query = query.Where(b => b.Amount <= 100);
                }
                // medium
                else if (svm.AmountRange == AmountRanges.Medium)
                {
                    query = query.Where(b => b.Amount >= 100);
                    query = query.Where(b => b.Amount <= 200);
                }
                // high
                else if (svm.AmountRange == AmountRanges.High)
                {
                    query = query.Where(b => b.Amount >= 200);
                    query = query.Where(b => b.Amount <= 300);
                }
                // highest
                else if (svm.AmountRange == AmountRanges.Highest)
                {
                    query = query.Where(b => b.Amount >= 300);
                }
                // custom range & an upper or lower limit is inputted
                else if (svm.AmountRange == AmountRanges.Custom && (svm.LowerLimit != null || svm.UpperLimit != null))
                {
                    if (svm.LowerLimit != null)
                    {
                        query = query.Where(b => b.Amount >= svm.LowerLimit);
                    }
                    if (svm.UpperLimit != null)
                    {
                        query = query.Where(b => b.Amount <= svm.UpperLimit);
                    }
                }
                
            }
            // date
            if (svm.DateRange != null)
            {
                // Last 15 days
                if (svm.DateRange == DateRanges.Last15)
                {
                    query = query.Where(b => b.Date >= DateTime.Today.AddDays(-15));
                }
                // last 30 days
                else if (svm.DateRange == DateRanges.Last30)
                {
                    query = query.Where(b => b.Date >= DateTime.Today.AddDays(-30));
                }
                // last 60 days
                else if (svm.DateRange == DateRanges.Last60)
                {
                    query = query.Where(b => b.Date >= DateTime.Today.AddDays(-60));
                }
                // custom range & an upper or lower limit is inputted
                else if (svm.DateRange == DateRanges.Custom && (svm.BeginningDate != null || svm.EndingDate != null))
                {
                    if (svm.BeginningDate != null)
                    {
                        query = query.Where(b => b.Date >= svm.BeginningDate);
                    }
                    if (svm.UpperLimit != null)
                    {
                        query = query.Where(b => b.Date <= svm.EndingDate);
                    }
                }
                // all
                //(query is not modified)
            }

            List<Transaction> SelectedTransactions = query.Include(b => b.Account).ToList();

            ViewBag.AllBookCount = _context.Transactions.Count();
            ViewBag.SelectedBookCount = SelectedTransactions.Count();

            return View("Index", SelectedTransactions.OrderByDescending(b => b.Amount));

        }

    }

}
