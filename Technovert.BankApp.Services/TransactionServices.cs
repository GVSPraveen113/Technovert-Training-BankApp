using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Services.Interfaces;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Services
{
    public class TransactionServices : ITransactionService
    {
        BankDbContext bankDb = new BankDbContext();
        public TransactionServices() { }

        public string Deposit(string bankId, string accountId, Transaction depositDTO)
        {
            Bank bank = SingleBank(bankId);
            Account account = SingleAccount(accountId);
            account.Balance += depositDTO.Amount;
            Transaction transaction = new Transaction()
            {
                TransactionId = GenerateTransactionId(bankId, account.AccountId),
                Amount = depositDTO.Amount,
                Type = (TransactionType.Credit),
                Mode = (TransactionMode.Deposit),
                SourceAccount = account,
                On = DateTime.Now
            };
            bankDb.Transactions.Add(transaction);
            bankDb.SaveChanges();
            return transaction.TransactionId;
        }
        public string Withdraw(string bankId, string accountId, Transaction transactionDTO)
        {
            Bank bank = SingleBank(bankId);
            Account account = SingleAccount(accountId);
            if (account.Balance >= transactionDTO.Amount)
            {
                account.Balance -= transactionDTO.Amount;

                Transaction transaction = new Transaction
                {
                    TransactionId = GenerateTransactionId(bankId, account.AccountId),
                    Amount = transactionDTO.Amount,
                    Type = (TransactionType.Debit),
                    SourceAccount = account,
                    Mode = (TransactionMode.Withdraw),
                    On = DateTime.Now
                };
                bankDb.Transactions.Add(transaction);
                bankDb.SaveChanges();
                return transaction.TransactionId;
            }
            else
            {
                throw new InsufficientBalanceException("Please Deposit some amount.Transaction failed due to insufficient Balance");
            }
        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            string dateTime = DateTime.Now.ToString("ddmmyyyy");
            if (bankId.Length < 3 || accountId.Length < 3)
            {
                throw new IncorrectArgumentRangeException(" BankId and AccountId Length must be greater than or equal to 3");
            }
            return "TXN" + bankId + accountId + dateTime;
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
        public Account SingleAccount(string accountId)
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

        public string TransferMoney(string sourceBankId, string sourceActId, string destinationBankId, TransactionCharge transactionCharge, Transaction transactionDTO)
        {
            Bank sourceBank = SingleBank(sourceBankId);
            Account sourceAccount = SingleAccount(sourceActId);
            if (sourceAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
            Bank destinationBank = SingleBank(sourceBankId);
            Account destinationAccount = SingleAccount(transactionDTO.DestinationAccountId);
            if (destinationAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
            decimal TaxPercentage = 0;
            if (transactionCharge == TransactionCharge.RTGS)
            {
                if (sourceBank.BankId == destinationBank.BankId)
                {
                    TaxPercentage = sourceBank.RTGSSameBank;
                }
                else
                {
                    TaxPercentage = sourceBank.RTGSDiffBank;
                }
            }
            else if (transactionCharge == TransactionCharge.IMPS)
            {
                if (destinationBank.BankId == destinationBank.BankId)
                {
                    TaxPercentage = sourceBank.IMPSSameBank;
                }
                else
                {
                    TaxPercentage = sourceBank.IMPSDiffBank;
                }
            }
            decimal amountTransfered = transactionDTO.Amount;
            decimal amountDeducted = amountTransfered + (amountTransfered * TaxPercentage) / 100;

            if (sourceAccount.Balance < amountDeducted)
            {
                throw new InsufficientBalanceException("Please Deposit some amount. Transaction failed due to insufficient Balance");
            }
            else
            {
                sourceAccount.Balance -= amountDeducted;
                destinationAccount.Balance += amountTransfered;
                Transaction t1 = new Transaction()
                {
                    TransactionId = GenerateTransactionId(sourceBankId, sourceAccount.AccountId),
                    DestinationAccountId = destinationAccount.AccountId,
                    DestinationAccount = destinationAccount,
                    Amount = amountDeducted,
                    Type = (TransactionType.Debit),
                    Mode = TransactionMode.Transfer,
                    On = DateTime.Now
                };
                Transaction t2 = new Transaction()
                {
                    TransactionId = GenerateTransactionId(destinationBankId, destinationAccount.AccountId),
                    SourceAccountId = sourceAccount.AccountId,
                    SourceAccount = sourceAccount,
                    Amount = amountTransfered,
                    Type = (TransactionType.Credit),
                    Mode = TransactionMode.Transfer,
                    On = DateTime.Now
                };
                bankDb.Transactions.Add(t1);
                bankDb.Transactions.Add(t2);
                bankDb.SaveChanges();
                return t1.TransactionId;
            }
        }
        public List<Transaction> GetTransactionsOfAccount(string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (Transaction transaction in bankDb.Transactions)
            {
                if (transaction.SourceAccountId == accountId || transaction.DestinationAccountId == accountId)
                {
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }
        public List<Transaction> GetAllTransactions()
        {
            return bankDb.Transactions.ToList();
        }
    }
}
