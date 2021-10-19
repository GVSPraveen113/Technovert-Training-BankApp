using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankServices
    {
        private List<Bank> banks;
        public BankServices()
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
            Bank res=this.banks.Find(m => m.Name == name);
            return res.Id;
        }
        public bool CheckBankExists(string name)
        {
            return this.banks.Any(b => b.Name == name);
        }

        public string CreateAccount(string BankId, string AccountHolderName, string Password, decimal InitialDeposit, bool Gender)
        {
            Bank bank = this.banks.Single(m => m.Id == BankId);
            Account account = new Account()
            {
                Name = AccountHolderName,
                Id = GenerateAccountId(AccountHolderName),
                Password = Password,
                Balance = InitialDeposit,
                isMale = Gender,
                Transactions = new List<Transaction>(),
                Status = (AccountStatus)TransactionType.Credit
            };   
            bank.Accounts.Add(account);
            return account.Id; 
        }
        public string Deposit(string BankName, string AccountId , decimal Deposit)
        {

            string BankId = GetBankId(BankName);
            Bank bank = this.banks.Single(m => m.Id == BankId);
            Account account = bank.Accounts.Single(ac => ac.Id == AccountId);
           
            account.Balance += Deposit;
            string retString = "The balance after Deposit is" + account.Balance;
            return retString;
        }

        public string Withdraw(string BankName, string AccountId, decimal Withdraw)
        {
            string BankId = GetBankId(BankName);
            Bank bank = this.banks.Single(m => m.Id == BankId);
            Account account = bank.Accounts.Single(ac => ac.Id == AccountId);
            account.Balance -= Withdraw;
            string retString = "The balance after Withdraw is" + account.Balance;
            return retString;
        }
        public string TransferMoney(string senderBankName,string senderActId,string receiverBankName,string receiverActId,decimal amount)
        {
            string sourceBankId = GetBankId(senderBankName);
            Bank senderbank = this.banks.Single(m => m.Id == sourceBankId);
            Account senderaccount = senderbank.Accounts.Single(ac => ac.Id == senderActId);
            string recieverBankId = GetBankId(receiverBankName);
            Bank receiverbank = this.banks.Single(m => m.Id == sourceBankId);
            Account receiveraccount = senderbank.Accounts.Single(ac => ac.Id == receiverActId);
            senderaccount.Balance -= amount;
            receiveraccount.Balance += amount;
            senderaccount.Transactions.Add(new Transaction()
            {
                Id = GenerateTransactionId(sourceBankId, senderaccount.Id),
                DestinationaccountId = receiveraccount.Id,
                Type = (TransactionType.Debit),
                On = DateTime.Now
            }) ;
            receiveraccount.Transactions.Add(new Transaction()
            {
                Id = GenerateTransactionId(recieverBankId, receiveraccount.Id),
                sourceAccountId = senderaccount.Id,
                Type = (TransactionType.Credit),
                On = DateTime.Now
            });

            string retString = "Money sucessfully transferred and your balance is"+senderaccount.Balance;
            return retString;
        }
        public List<Transaction> ShowTransactions(string BankName, string AccountId) 
        {
            List<Transaction> transactions = new List<Transaction>();
            Bank bank = this.banks.Single(m => m.Name == BankName);
            Account account = bank.Accounts.Single(ac => ac.Id == AccountId);
            return account.Transactions;

        }
            private string GenerateRandomBankId(string BankName)
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
            return BankName.Substring(0, 3) + date;

        }
        private string GenerateAccountId(string AccountHolderName)
        {
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            return AccountHolderName.Substring(0, 3) + date;
        }
        public string GenerateTransactionId(string BankId, string AccountId)
        {
            DateTime dt = new DateTime();
            return "TXN" + BankId + AccountId + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy");
        }
    }
}
