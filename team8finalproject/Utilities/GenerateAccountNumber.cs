﻿using team8finalproject.DAL;
using System;
using System.Linq;


namespace team8finalproject.Utilities
{
    public static class GenerateAccountNumber
    {
        public static string GetNextAccountNumber(AppDbContext _context)
        {
            Int32 intMaxAccountNumber; //the current maximum number
            Int32 intNextAccountNumber; //the trans number

            if (_context.Products.Count() == 0) //there are no Accounts in the database yet
            {
                intMaxAccountNumber = 1000000001; //Account number starts at 1
            }
            else
            {
                intMaxAccountNumber = _context.Products.Max(c => c.ProductID); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextAccountNumber = intMaxAccountNumber + 1;
			string NextAccountNumber = intNextAccountNumber.ToString();
			// gets substring
			NextAccountNumber.Substring(NextAccountNumber.Length - 4);
			NextAccountNumber = "XXXXXX" + NextAccountNumber;
			//return the value
			return NextAccountNumber;
        }

    }
}