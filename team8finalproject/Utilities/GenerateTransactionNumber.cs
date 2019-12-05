using team8finalproject.DAL;
using System;
using System.Linq;


namespace team8finalproject.Utilities
{
    public static class GenerateTransactionNumber
    {
        public static Int64 GetNextTransactionNumber(AppDbContext _context)
        {
            Int64 intMaxTransactionNumber; //the current maximum number
            Int64 intNextTransactionNumber; //the trans number

            if (_context.Transactions.Count() == 0) //there are no transactions in the database yet
            {
                intMaxTransactionNumber = 1; //transaction number starts at 1
            }
            else
            {
                intMaxTransactionNumber = _context.Transactions.Max(c => c.TransactionID); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextTransactionNumber = intMaxTransactionNumber + 1;

            //return the value
            return intNextTransactionNumber;
        }

    }
}