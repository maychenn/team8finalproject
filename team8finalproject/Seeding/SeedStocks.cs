using team8finalproject.Models;
using team8finalproject.DAL;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace team8finalproject.Seeding
{
    public static class SeedStocks
    {
        public static void SeedAllStocks(AppDbContext db)
        {
            if (db.Stocks.Count() == 20)
            {
                throw new NotSupportedException("The database already contains all 6 Stocks!");
            }

            Int32 intStocksAdded = 0;
            String strStockTicker = "Begin"; //helps to keep track of error on Stocks
            List<Stock> Stock = new List<Stock>();

            try
            {
                Stock e1 = new Stock()
                {
                    Ticker = "GOOG",
                    StockType = StockType.Ordinary,
                    StockName = "Alphabet Inc.",
                    Price = 1315.20m,
                    Fee = 25
                };
                Stock.Add(e1);

                Stock e2 = new Stock()
                {
                    Ticker = "AAPL",
                    StockType = StockType.Ordinary,
                    StockName = "Apple",
                    Price = 266.10m,
                    Fee = 40
                };
                Stock.Add(e2);

                Stock e3 = new Stock()
                {
                    Ticker = "AMZN",
                    StockType = StockType.Ordinary,
                    StockName = "Amazon.com Inc.",
                    Price = 1755.03m,
                    Fee = 15
                };
                Stock.Add(e3);

                Stock e4 = new Stock()
                {
                    Ticker = "LUV",
                    StockType = StockType.Ordinary,
                    StockName = "Southwest Airlines",
                    Price = 57.70m,
                    Fee = 35
                };
                Stock.Add(e4);

                Stock e5 = new Stock()
                {
                    Ticker = "TXN",
                    StockType = StockType.Ordinary,
                    StockName = "Texas Instruments",
                    Price = 117.89m,
                    Fee = 15
                };
                Stock.Add(e5);

                Stock e6 = new Stock()
                {
                    Ticker = "HSY",
                    StockType = StockType.Ordinary,
                    StockName = "The Hershey Company",
                    Price = 146.42m,
                    Fee = 25
                };
                Stock.Add(e6);

                Stock e7 = new Stock()
                {
                    Ticker = "V",
                    StockType = StockType.Ordinary,
                    StockName = "Visa Inc.",
                    Price = 181.92m,
                    Fee = 10
                };
                Stock.Add(e7);

                Stock e8 = new Stock()
                {
                    Ticker = "NKE",
                    StockType = StockType.Ordinary,
                    StockName = "Nike",
                    Price = 93.44m,
                    Fee = 30
                };
                Stock.Add(e8);

                Stock e9 = new Stock()
                {
                    Ticker = "VWO",
                    StockType = StockType.ETF,
                    StockName = "Vanguard Emerging Markets ETF",
                    Price = 42.40m,
                    Fee = 20
                };
                Stock.Add(e9);

                Stock e10 = new Stock()
                {
                    Ticker = "F",
                    StockType = StockType.Ordinary,
                    StockName = "Ford Motor Company",
                    Price = 8.93m,
                    Fee = 10
                };
                Stock.Add(e10);

                Stock e11 = new Stock()
                {
                    Ticker = "BAC",
                    StockType = StockType.Ordinary,
                    StockName = "Bank of America Corporation",
                    Price = 32.94m,
                    Fee = 10
                };
                Stock.Add(e11);

                Stock e12 = new Stock()
                {
                    Ticker = "VNQ",
                    StockType = StockType.ETF,
                    StockName = "Vanguard REIT ETF",
                    Price = 93.22m,
                    Fee = 15
                };
                Stock.Add(e12);

                Stock e13 = new Stock()
                {
                    Ticker = "KMX",
                    StockType = StockType.Ordinary,
                    StockName = "CarMax, Inc.",
                    Price = 99.94m,
                    Fee = 15
                };
                Stock.Add(e13);

                Stock e14 = new Stock()
                {
                    Ticker = "DIA",
                    StockType = StockType.IndexFund,
                    StockName = "Dow Jones Industrial Average Index Fund",
                    Price = 279.27m,
                    Fee = 25
                };
                Stock.Add(e14);

                Stock e15 = new Stock()
                {
                    Ticker = "SPY",
                    StockType = StockType.IndexFund,
                    StockName = "S&P 500",
                    Price = 311.95m,
                    Fee = 25
                };
                Stock.Add(e15);

                Stock e16 = new Stock()
                {
                    Ticker = "BEN",
                    StockType = StockType.Ordinary,
                    StockName = "Franklin Resources, Inc.",
                    Price = 27.84m,
                    Fee = 25
                };
                Stock.Add(e16);

                Stock e17 = new Stock()
                {
                    Ticker = "PGSCX",
                    StockType = StockType.MutualFund,
                    StockName = "Pacific Advisors Small Cap Value Fund",
                    Price = 15.95m,
                    Fee = 15
                };
                Stock.Add(e17);

                Stock e18 = new Stock()
                {
                    Ticker = "DIS",
                    StockType = StockType.Ordinary,
                    StockName = "Disney",
                    Price = 148.98m,
                    Fee = 20
                };
                Stock.Add(e18);

                Stock e19 = new Stock()
                {
                    Ticker = "USAWX",
                    StockType = StockType.MutualFund,
                    StockName = "USAA World Growth Fund",
                    Price = 34.49m,
                    Fee = 15
                };
                Stock.Add(e19);

                Stock e20 = new Stock()
                {
                    Ticker = "CGLOX",
                    StockType = StockType.MutualFund,
                    StockName = "Capital Group Global Equity Fund",
                    Price = 16.72m,
                    Fee = 25
                };
                Stock.Add(e20);

                try
                {
                    foreach (Stock StockToAdd in Stock)
                    {
                        strStockTicker = StockToAdd.Ticker;
                        Stock dbStock = db.Stocks.FirstOrDefault(b => b.Ticker == StockToAdd.Ticker);
                        if (dbStock == null) //this Stock doesn't exist
                        {
                            db.Stocks.Add(StockToAdd);
                            db.SaveChanges();
                            intStocksAdded += 1;
                        }
                        else //Stock exists - update values
                        {
                            dbStock.Ticker = StockToAdd.Ticker;
                            dbStock.StockType = StockToAdd.StockType;
                            dbStock.StockName = StockToAdd.StockName;
                            dbStock.Price = StockToAdd.Price;
                            dbStock.Fee = StockToAdd.Fee;
                            db.Update(dbStock);
                            db.SaveChanges();
                            intStocksAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String msg = "  Repositories added:" + intStocksAdded + "; Error on " + strStockTicker;
                    throw new InvalidOperationException(ex.Message + msg);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
