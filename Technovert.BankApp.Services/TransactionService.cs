using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;


namespace Technovert.BankApp.Services
{
    public class TransactionService
    {
        //AccountService as = new AccountService();
        private readonly BankService bankservice;
        private readonly AccountService accountservice;
       
        public TransactionService(BankService bankservice)
        {
            this.bankservice = bankservice;
        }
        
        public bool Deposit(string bankName, string accountId, string password, decimal deposit)
        {

            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);

            Account account = accountservice.SingleAccount(bank, accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                account.Balance += deposit;
                account.Transactions.Add(new Transaction()
                {
                    Id = GenerateTransactionId(bankId, account.Id),
                    Type = (TransactionType.Credit),
                    On = DateTime.Now
                });
                return true;
            }
            else
            {
                throw new IncorrectPasswordException("Password is incorrect!!");
            }
                

            
        }

        public bool Withdraw(string bankName, string accountId, string password, decimal withdraw)
        {
            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);
            Account account = accountservice.SingleAccount(bank, accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                if (account.Balance >= withdraw)
                {
                    account.Balance -= withdraw;
                    account.Transactions.Add(new Transaction()
                    {
                        Id = GenerateTransactionId(bankId, account.Id),
                        Type = (TransactionType.Debit),
                        On = DateTime.Now
                    });
                    return true;
                }
                else
                {
                    throw new InsufficientBalanceException("Please Deposit some amount.Transaction failed due to insufficient Balance");
                }
            }
            else
            {
                throw new IncorrectPasswordException("Enter Correct Password! ");
            }
                
            
        }
        public bool TransferMoney(string senderBankName, string senderActId, string password, string receiverBankName, string receiverActId, TransactionCharge transactionCharge, decimal amountTransfered)
        {
            string sourceBankId = bankservice.GetBankId(senderBankName);
            Bank senderBank = bankservice.SingleBank(sourceBankId);
            Account senderAccount = senderBank.Accounts.Single(ac => ac.Id == senderActId);
            string recieverBankId = bankservice.GetBankId(receiverBankName);
            Bank receiverBank = bankservice.SingleBank(recieverBankId);
            Account receiveraccount = senderBank.Accounts.Single(ac => ac.Id == receiverActId);
            decimal TaxPercentage = 0;
            if (transactionCharge == TransactionCharge.RTGS)
            {
                if (senderBank.Id == receiverBank.Id)
                {
                    TaxPercentage = senderBank.RTGSSameBank;
                }
                else
                {
                    TaxPercentage = senderBank.RTGSDiffBank;
                }
            }
            else if (transactionCharge == TransactionCharge.IMPS)
            {
                if (senderBank.Id == receiverBank.Id)
                {
                    TaxPercentage = senderBank.IMPSSameBank;
                }
                else
                {
                    TaxPercentage = senderBank.IMPSDiffBank;
                }
            }
            decimal amountDeducted = amountTransfered + (amountTransfered * TaxPercentage) / 100;
            if (accountservice.ValidatePassword(senderAccount, password))
            {
                if (senderAccount.Balance < amountDeducted)
                {
                    throw new InsufficientBalanceException("Please Deposit some amount. Transaction failed due to insufficient Balance");
                }
                else
                {
                    senderAccount.Balance -= amountDeducted;
                    receiveraccount.Balance += amountTransfered;
                    senderAccount.Transactions.Add(new Transaction()
                    {
                        Id = GenerateTransactionId(sourceBankId, senderAccount.Id),
                        DestinationaccountId = receiveraccount.Id,
                        Amount = amountDeducted,
                        Type = (TransactionType.Debit),
                        On = DateTime.Now
                    });
                    receiveraccount.Transactions.Add(new Transaction()
                    {
                        Id = GenerateTransactionId(recieverBankId, receiveraccount.Id),
                        sourceAccountId = senderAccount.Id,
                        Amount = amountTransfered,
                        Type = (TransactionType.Credit),
                        On = DateTime.Now
                    });
                    return true;
                }
            }
            else
            {
                throw new IncorrectPasswordException("Check your Password!");
            }
        }
        public List<Transaction> GetTransactions(string bankName, string accountId, string password)
        {
            List<Transaction> transactions = new List<Transaction>();
            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);
            Account account = bank.Accounts.Single(ac => ac.Id == accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                return account.Transactions;
            }
            else
            {
                throw new IncorrectPasswordException("Incorrect Password! Enter correct password to retrieve your transactions");
            }

        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            DateTime dt = new DateTime();
            if(bankId.Length<3 || accountId.Length < 3)
            {
                throw new IncorrectArgumentRangeException(" BankId and AccountId Length must be greater than or equal to 3");
            }
            return "TXN" + bankId + accountId + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy");
        }

    }
}
