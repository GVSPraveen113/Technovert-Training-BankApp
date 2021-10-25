using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankService
    {
        /**/public List<Bank> banks { get; set; }
        public BankService()
        {
            this.banks = new List<Bank>();
        }
        public string CreateBank(string name)
        {
            if (!CheckBankExists(name))
            {
                Bank bank = new Bank
                {
                    Id = this.GenerateRandomBankId(name),
                    Name = name,
                    Accounts = new List<Account>()
                };
                banks.Add(bank);
                return bank.Id;
            }
            else
            {
                throw new BankCreationException();
            }
        }
        public string GetBankId(string name)
        {
            Bank bank=this.banks.Find(m => m.Name == name);
            return bank.Id;
        }
        private bool CheckBankExists(string name)
        {
            return this.banks.Any(b => b.Name == name);
        }
        public Bank BankSingle(string bankId)
        {
            return this.banks.Single(m => m.Id == bankId);
        }

        
        private string GenerateRandomBankId(string bankName)
        {
            /*Random rnd = new Random();
            int newrnd = rnd.Next(1000,9999);
            if (this.banks.Any(m => m.Id == newrnd))
            {
                return GenerateRandomBankId();
            }
            return newrnd;*/
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            return bankName.Substring(0, 3) + date;

        }
        public string CreateAccount(string bankId, string accountHolderName, string password, decimal initialDeposit, bool gender)
        {
            Bank bank = this.BankSingle(bankId);
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
        private string GenerateAccountId(string accountHolderName)
        {
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            return accountHolderName.Substring(0, 3) + date;
        }


    }
}
