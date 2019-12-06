using Microsoft.AspNetCore.Mvc;
using team8finalproject.DAL;
using System;


namespace team8finalproject.Controllers


{

   public class SeedController : Controller

    {
        private AppDbContext _db;
		private IServiceProvider serviceProvider;

        public SeedController(AppDbContext context)


        {
            _db = context;

        }

        // GET: /<controller>/


        public IActionResult Index()

        {
            return View();

        }

        public IActionResult SeedStocks()

        {
            try

            {
                Seeding.SeedStocks.SeedAllStocks(_db);

            }

            catch (NotSupportedException ex)

            {

                return View("Error", new String[] { "The stocks have already been added", ex.Message });
            }

            catch (InvalidOperationException ex)

            {

                return View("Error", new String[] { "There was an error adding stocks to the database", ex.Message });

            }

            return View("Confirm");

        }
	}

}
