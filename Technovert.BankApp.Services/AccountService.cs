using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.Services
{
    public class AccountService: IAccountService
    {
        BankDbContext bankDb = new BankDbContext();
        public AccountService()
        {
        
        }
        public string CreateAccount(string bankId, Account accountDTO/*string accountHolderName, string password, decimal initialDeposit, bool gender*/)
        {
            Bank bank = bankDb.Banks.SingleOrDefault(b => b.BankId == bankId);

            Account account = new Account()
            {
                Name = accountDTO.Name,
                AccountId = GenerateAccountId(accountDTO.Name),
                Password = accountDTO.Password,
                Balance = accountDTO.Balance,
                isMale = accountDTO.isMale,
                Bank = bank,
                Transactions = new List<Transaction>(),
                Status = (AccountStatus)TransactionType.Credit
            };
            //bank.Accounts.Add(account);
            bankDb.Accounts.Add(account);
            bankDb.SaveChanges();
            return account.AccountId;
        }
        public bool UpdateAccount(string bankId, string accountId, Account accountDTO)
        {
            Bank bank = SingleBank(bankId);
            Account account = bankDb.Accounts.Where(acc => acc.AccountId == accountId).First();
            account.Name = accountDTO.Name;
            account.isMale = accountDTO.isMale;
            //bankService.saveJson();
            bankDb.SaveChanges();
            return true;
        }
        public bool DeleteAccount(string bankId, string accountId)
        {
            Bank bank = SingleBank(bankId);
            Account account = bankDb.Accounts.SingleOrDefault(account => account.AccountId == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException("Account isn't found!");
            }

            //bank.Accounts.Remove(account);
            //bankService.saveJson();
            bankDb.Accounts.Remove(account);
            bankDb.SaveChanges();
            return true;
        }
        public Bank SingleBank(string bankId)
        {
            List<Bank> banksList = bankDb.Banks.ToList();
            Bank bank = banksList.SingleOrDefault(m => m.BankId == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("The Bank details provided are incorrect. Check details again");
            }
            else
            {
                return bank;
            }
        }
        public Account SingleAccount(Bank bank, string accountId)
        {
            Account account = bankDb.Accounts.SingleOrDefault(m => m.AccountId == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException("Account is not Found in the Database.Please recheck your credentials or create a new account");
            }
            else
            {
                return account;
            }
        }
        public List<Account> GetAllAccounts(string bankId)
        {
            Bank bank = SingleBank(bankId);
            List<Account> Accounts = new List<Account>();
            foreach (Account account in bankDb.Accounts)
            {
                if (account.Bank.BankId == bankId)
                {
                    Accounts.Add(account);
                }
            }
            return Accounts;

        }
        public bool ValidatePassword(Account account, string password)
        {
            if (account.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GenerateAccountId(string accountHolderName)
        {
            string dateTime = DateTime.Now.ToString("ddmmyyyy");
            return accountHolderName.Substring(0, 3) + dateTime;
        }
    }
}
