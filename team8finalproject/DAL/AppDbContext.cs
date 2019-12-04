using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using team8finalproject.Models;

namespace team8finalproject.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<StandardAccount> StandardAccounts { get; set; }
        public DbSet<IRA> IRAs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<PayBill> PayBills { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<PortfolioDetail> PortfolioDetails { get; set; }

    }
}