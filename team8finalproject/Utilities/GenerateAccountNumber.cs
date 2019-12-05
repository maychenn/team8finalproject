using team8finalproject.DAL;
using System;
using System.Linq;


namespace team8finalproject.Utilities
{
    public static class GenerateAccountNumber
    {
        public static Int32 GetNextAccountNumber(AppDbContext _context)
        {
            Int32 intMaxAccountNumber;
            Int32 intNextAccountNumber = 1000000000;

            intMaxAccountNumber = _context.Products.Max(c => c.ProductID); //this is the highest number in the database right now
           

            //add one to the current max to find the next one
            intNextAccountNumber += (intMaxAccountNumber + 1);

            //return the value
            return intNextAccountNumber;
        }

    }
}