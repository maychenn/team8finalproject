using team8finalproject.DAL;
using System;
using System.Linq;


namespace team8finalproject.Utilities
{
    public static class GenerateAccountNumber
    {
        public static Int32 GetNextAccountNumber(AppDbContext _context)
        {
            Int32 intMaxAccountNumber; //the current maximum number
            Int32 intNextAccountNumber; //the trans number

            if (_context.Products.Count() == 0) //there are no Accounts in the database yet
            {
                intMaxAccountNumber = 1000000000; //Account number starts at 1
            }
            else
            {
                intMaxAccountNumber = _context.Products.Max(c => c.AccountNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextAccountNumber = intMaxAccountNumber + 1;

			//return the value
			return intNextAccountNumber;
        }

    }
}