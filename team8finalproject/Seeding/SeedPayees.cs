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
    public static class SeedPayees
    {
        public static void SeedAllPayees(AppDbContext db)
        {
            if (db.Payees.Count() == 6)
            {
                throw new NotSupportedException("The database already contains all 6 Payees!");
            }

            Int32 intPayeesAdded = 0;
            String strPayeePayeeName = "Begin"; //helps to keep track of error on Payees
            List<Payee> Payee = new List<Payee>();

            try
            {
                Payee p1 = new Payee()
                {
                    Name = "City of Austin Water",
                    Selection = PayeeType.Utilities,
                    Address = "113SouthCongressAve.",
                    City = "Austin",
                    State = "TX",
                    Zip = "78710",
                    PhoneNumber = "5126645558"
                };
                Payee.Add(p1);

                Payee p2 = new Payee()
                {
                    Name = "ReliantEnergy",
                    Selection = PayeeType.Utilities,
                    Address = "3500 E.Interstate10 .",
                    City = "Houston",
                    State = "TX",
                    Zip = "77099",
                    PhoneNumber = "7135546697"
                };
                Payee.Add(p2);

                Payee p3 = new Payee()
                {
                    Name = "LeeProperties",
                    Selection = PayeeType.Rent,
                    Address = "2500Salado.",
                    City = "Austin",
                    State = "TX",
                    Zip = "78705",
                    PhoneNumber = "5124453312"
                };
                Payee.Add(p3);

                Payee p4 = new Payee()
                {
                    Name = "CapitalOne",
                    Selection = PayeeType.CreditCard,
                    Address = "1299FargoBlvd.",
                    City = "Cheyenne",
                    State = "WY",
                    Zip = "82001",
                    PhoneNumber = "5302215542"
                };
                Payee.Add(p4);


                Payee p5 = new Payee()
                {
                    Name = "Vanguard PayeeName",
                    Selection = PayeeType.Mortgage,
                    Address = "10976 Interstate 35 South",
                    City = "Austin",
                    State = "TX",
                    Zip = "78745",
                    PhoneNumber = "5128654951"
                };
                Payee.Add(p5);

                Payee p6 = new Payee()
                {
                    Name = "Lawn Care of Texas",
                    Selection = PayeeType.Other,
                    Address = " 4473 W.3rd Street",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PhoneNumber = "5123365247"
                };
                Payee.Add(p6);

                try
                {
                    foreach (Payee PayeeToAdd in Payee)
                    {
                        strPayeePayeeName = PayeeToAdd.Name;
                        Payee dbPayee = db.Payees.FirstOrDefault(b => b.Name == PayeeToAdd.Name);
                        if (dbPayee == null) //this PayeeName doesn't exist
                        {
                            db.Payees.Add(PayeeToAdd);
                            db.SaveChanges();
                            intPayeesAdded += 1;
                        }
                        else //Payee exists - update values
                        {
                            dbPayee.Name = PayeeToAdd.Name;
                            dbPayee.Selection = PayeeToAdd.Selection;
                            dbPayee.Address = PayeeToAdd.Address;
                            dbPayee.City = PayeeToAdd.City;
                            dbPayee.State = PayeeToAdd.State;
                            dbPayee.Zip = PayeeToAdd.Zip;
                            dbPayee.PhoneNumber = PayeeToAdd.PhoneNumber;
                            db.Update(dbPayee);
                            db.SaveChanges();
                            intPayeesAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String msg = "  Repositories added:" + intPayeesAdded + "; Error on " + strPayeePayeeName;
                    throw new InvalidOperationException(ex.Message + msg);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        internal static object SeedAllPayees(IServiceProvider service)
        {
            throw new NotImplementedException();
        }
    }
}
