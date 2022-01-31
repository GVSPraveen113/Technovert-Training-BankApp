using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Technovert.BankApp.Services
{
    public class BankDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=AutomatedTellerMachine;Integrated Security=True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Bank>()
                .HasMany<Account>(b => b.Accounts)
                .WithOne(a => a.Bank);

            //modelBuilder.Entity<Bank>().HasMany<CurrencyType>(b => b.)

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceAccount)
                .WithMany(sa => sa.Transactions)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            /*modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationAccount)
                .WithMany(sa=>sa.Transactions)
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);*/
        }

    }
}