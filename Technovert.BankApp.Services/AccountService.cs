using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class AccountService
    {
        private readonly BankService bs;
        public AccountService(BankService bs)
        {
            this.bs = bs;
        }
        public string CreateAccount(string bankId, string accountHolderName, string password, decimal initialDeposit, bool gender)
        {
            
            Bank bank = bs.SingleBank(bankId);
            
            Account account = new Account()
            {
                Name = accountHolderName,
                Id = GenerateAccountId(accountHolderName),
                Password = password,
                Balance = initialDeposit,
                isMale = gender,
                Transactions = new List<Transaction>(),
                //Status = (AccountStatus)TransactionType.Credit
            };
            bank.Accounts.Add(account);
            return account.Id;
        }
        public Account SingleAccount(Bank bank,string accountId)
        {
            Account account = bank.Accounts.SingleOrDefault(m => m.Id == accountId);
            if (account == null)
            {
                throw new BankNotFoundException();
            }
            else
            {
                return account;
            }
        }
        private string GenerateAccountId(string accountHolderName)
        {
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            return accountHolderName.Substring(0, 3) + date;
        }
    }
}
