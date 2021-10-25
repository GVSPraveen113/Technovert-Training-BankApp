using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class TransactionService
    {
        //AccountService as = new AccountService();
        
        public bool Deposit(BankService bs,string bankName, string accountId, decimal deposit)
        {

            string BankId = bs.GetBankId(bankName);
            Bank bank = bs.BankSingle(BankId);
            //throw new BankNotFoundException();

            Account account = bank.Accounts.Single(ac => ac.Id == accountId);

            account.Balance += deposit;
            return true;
        }

        public bool Withdraw(BankService bs,string bankName, string accountId, decimal withdraw)
        {
            string bankId = bs.GetBankId(bankName);
            Bank bank = bs.BankSingle(bankId);
            Account account = bank.Accounts.Single(ac => ac.Id == accountId);

            if (account.Balance >= withdraw)
            {
                account.Balance -= withdraw;
                return true;
            }
            else
            {
                throw new InsufficientBalanceException();
            }
        }
        public bool TransferMoney(BankService bs,string senderBankName, string senderActId, string receiverBankName, string receiverActId, decimal amount)
        {
            string sourceBankId = bs.GetBankId(senderBankName);
            Bank senderbank = bs.BankSingle(sourceBankId);
            Account senderaccount = senderbank.Accounts.Single(ac => ac.Id == senderActId);
            string recieverBankId = bs.GetBankId(receiverBankName);
            Bank receiverbank = bs.BankSingle(recieverBankId);
            Account receiveraccount = senderbank.Accounts.Single(ac => ac.Id == receiverActId);
            senderaccount.Balance -= amount;
            receiveraccount.Balance += amount;
            senderaccount.Transactions.Add(new Transaction()
            {
                Id = GenerateTransactionId(sourceBankId, senderaccount.Id),
                DestinationaccountId = receiveraccount.Id,
                Type = (TransactionType.Debit),
                On = DateTime.Now
            });
            receiveraccount.Transactions.Add(new Transaction()
            {
                Id = GenerateTransactionId(recieverBankId, receiveraccount.Id),
                sourceAccountId = senderaccount.Id,
                Type = (TransactionType.Credit),
                On = DateTime.Now
            });
            return true;
        }
        public List<Transaction> GetTransactions(BankService bs, string bankName, string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string bankId = bs.GetBankId(bankName);
            Bank bank = bs.BankSingle(bankId);
            Account account = bank.Accounts.Single(ac => ac.Id == accountId);
            return account.Transactions;

        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            DateTime dt = new DateTime();
            return "TXN" + bankId + accountId + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy");
        }

    }
}
