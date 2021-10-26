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
        
        public bool Deposit(string bankName, string accountId, decimal deposit)
        {

            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);

            Account account = accountservice.SingleAccount(bank, accountId);
                

            account.Balance += deposit;
            account.Transactions.Add(new Transaction()
            {
                Id = GenerateTransactionId(bankId, account.Id),
                Type = (TransactionType.Credit),
                On = DateTime.Now
            });
            return true;
        }

        public bool Withdraw(string bankName, string accountId, decimal withdraw)
        {
            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);
            Account account = accountservice.SingleAccount(bank, accountId);

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
                throw new InsufficientBalanceException();
            }
        }
        public bool TransferMoney(string senderBankName, string senderActId, string receiverBankName, string receiverActId, TransactionCharge transactionCharge, decimal amountTransfered)
        {
            string sourceBankId = bankservice.GetBankId(senderBankName);
            Bank senderBank = bankservice.SingleBank(sourceBankId);
            Account senderAccount = senderBank.Accounts.Single(ac => ac.Id == senderActId);
            string recieverBankId = bankservice.GetBankId(receiverBankName);
            Bank receiverBank = bankservice.SingleBank(recieverBankId);
            Account receiveraccount = senderBank.Accounts.Single(ac => ac.Id == receiverActId);
            decimal TaxPercentage=0;
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
            else if(transactionCharge == TransactionCharge.IMPS)
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
            decimal amountDeducted = amountTransfered + (amountTransfered * TaxPercentage)/100;
            


            if (senderAccount.Balance < amountDeducted)
            {
                throw new InsufficientBalanceException();
            }
            else
            {
                senderAccount.Balance -= amountDeducted;
                receiveraccount.Balance += amountTransfered;
                senderAccount.Transactions.Add(new Transaction()
                {
                    Id = GenerateTransactionId(sourceBankId, senderAccount.Id),
                    DestinationaccountId = receiveraccount.Id,
                    Amount=amountDeducted,
                    Type = (TransactionType.Debit),
                    On = DateTime.Now
                });
                receiveraccount.Transactions.Add(new Transaction()
                {
                    Id = GenerateTransactionId(recieverBankId, receiveraccount.Id),
                    sourceAccountId = senderAccount.Id,
                    Amount=amountTransfered,
                    Type = (TransactionType.Credit),
                    On = DateTime.Now
                });
                return true;
            }
        }
        public List<Transaction> GetTransactions(string bankName, string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string bankId = bankservice.GetBankId(bankName);
            Bank bank = bankservice.SingleBank(bankId);
            Account account = bank.Accounts.Single(ac => ac.Id == accountId);
            return account.Transactions;

        }
        public string GenerateTransactionId(string bankId, string accountId)
        {
            DateTime dt = new DateTime();
            if(bankId.Length<3 || accountId.Length < 3)
            {
                throw new IncorrectArgumentRangeException();
            }
            return "TXN" + bankId + accountId + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy");
        }

    }
}
