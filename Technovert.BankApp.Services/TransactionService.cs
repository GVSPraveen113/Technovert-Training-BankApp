using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;


namespace Technovert.BankApp.Services
{
    public class TransactionService
    {
        //AccountService as = new AccountService();
        private readonly BankService bankService;
        private readonly AccountService accountservice;
       
        public TransactionService(BankService bankService,AccountService accountService)
        {
            this.bankService = bankService;
            this.accountservice = accountService;
        }
        
         
        public bool Deposit(string bankId, string accountId, string password, string currencyName, decimal deposit)
        {
            Bank bank = bankService.SingleBank(bankId);
            decimal toINRConversion;
            if (bank.CurrenciesAccepted.ContainsKey(currencyName))
            {
                toINRConversion = bank.CurrenciesAccepted[currencyName];
            }
            else
            {
                throw new BankNotFoundException("The following Currency Not Found!");
            }
            Account account = accountservice.SingleAccount(bank, accountId);
            deposit = deposit * toINRConversion;
            if (accountservice.ValidatePassword(account, password))
            {
                account.Balance += deposit;
                account.Transactions.Add(new Transaction()
                {
                    Id = GenerateTransactionId(bankId, account.Id),
                    Amount=deposit,
                    Type = (TransactionType.Credit),
                    On = DateTime.Now
                });
                /*string jsonTransaction = JsonSerializer.Serialize(transaction);
                File.AppendAllText(@"F:\Visual Studio Code Projects\Technovert.BankApp\transactions.json", jsonTransaction);*/
                bankService.saveJson();
                Console.WriteLine(account.Balance);
                return true;
            }
            else
            {
                throw new IncorrectPasswordException("Password is incorrect!!");
            }
            
        }
        

        public bool Withdraw(string bankId, string accountId, string password, decimal withdraw)
        {
            Bank bank = bankService.SingleBank(bankId);
            Account account = accountservice.SingleAccount(bank, accountId);
            if (accountservice.ValidatePassword(account, password))
            {
                if (account.Balance >= withdraw)
                {
                    account.Balance -= withdraw;
                   
                    Transaction transaction=new Transaction
                    {
                        Id = GenerateTransactionId(bankId, account.Id),
                        Amount=withdraw,
                        Type = (TransactionType.Debit),
                        On = DateTime.Now
                    };
                    account.Transactions.Add(transaction);
                    string jsonTransaction = JsonSerializer.Serialize(transaction);
                    File.AppendAllText(@"F:\Visual Studio Code Projects\Technovert.BankApp\transactions.json", jsonTransaction);


                   Console.WriteLine(account.Balance);
                    bankService.saveJson();
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
        public bool TransferMoney(string senderBankId, string senderActId, string password, string recieverBankId, string receiverActId, TransactionCharge transactionCharge, decimal amountTransfered)
        {
            Bank senderBank = bankService.SingleBank(senderBankId);
            Account senderAccount = senderBank.Accounts.SingleOrDefault(ac => ac.Id == senderActId);
            if (senderAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
            Bank receiverBank = bankService.SingleBank(recieverBankId);
            Account receiverAccount = senderBank.Accounts.SingleOrDefault(ac => ac.Id == receiverActId);
            if (receiverAccount == null)
            {
                throw new AccountNotFoundException("Account Not Found! Please Check");
            }
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
                    receiverAccount.Balance += amountTransfered;
                    senderAccount.Transactions.Add(new Transaction()
                    {
                        Id = GenerateTransactionId(senderBankId, senderAccount.Id),
                        DestinationaccountId = receiverAccount.Id,
                        Amount = amountDeducted,
                        Type = (TransactionType.Debit),
                        On = DateTime.Now
                    });
                    receiverAccount.Transactions.Add(new Transaction()
                    {
                        Id = GenerateTransactionId(recieverBankId, receiverAccount.Id),
                        sourceAccountId = senderAccount.Id,
                        Amount = amountTransfered,
                        Type = (TransactionType.Credit),
                        On = DateTime.Now
                    });
                    /*string jsonTransaction = JsonSerializer.Serialize(transaction);
                    File.AppendAllText(@"F:\Visual Studio Code Projects\Technovert.BankApp\transactions.json", jsonTransaction);*/
                    Console.WriteLine(senderAccount.Balance);
                    Console.WriteLine(receiverAccount.Balance);
                    bankService.saveJson();
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
            string bankId = bankService.GetBankId(bankName);
            Bank bank = bankService.SingleBank(bankId);
            Account account = bank.Accounts.SingleOrDefault(ac => ac.Id == accountId);
            if (account == null)
            {
                throw new AccountNotFoundException("Account Not Found!");
            }
            if (accountservice.ValidatePassword(account, password))
            {
                return account.Transactions;
            }
            else
            {
                throw new IncorrectPasswordException("Incorrect Password! Enter correct password to retrieve your transactions");
            }

        }
        public bool RevertTransaction(string bankId, string accountId, string transactionId)
        {
            Bank bank = bankService.SingleBank(bankId);
            Account account = bank.Accounts.SingleOrDefault(ac => ac.Id == accountId);
            
            if (account == null)
            {
                throw new AccountNotFoundException("Account may have been removed!");
            }
            Transaction transaction = account.Transactions.SingleOrDefault(tr => tr.Id == transactionId);
            if (transaction == null)
            {
                throw new TransactionNotFoundException("Transaction not Found!");
            }
            Account alternateAccount = bank.Accounts.SingleOrDefault(ac => ac.Id == transaction.DestinationaccountId);
            if (alternateAccount == null)
            {
                throw new AccountNotFoundException("Account may have been removed!");
            }
            if (transaction.Type == TransactionType.Credit)
            {
                account.Transactions.Remove(transaction);
                account.Balance -= transaction.Amount;
                alternateAccount.Balance += transaction.Amount;
                alternateAccount.Transactions.Remove(transaction);
            }
            if (transaction.Type == TransactionType.Debit)
            {
                account.Balance += transaction.Amount;
                account.Transactions.Remove(transaction);
                alternateAccount.Balance -= transaction.Amount;
                alternateAccount.Transactions.Remove(transaction);
            }
            bankService.saveJson();
            return true;
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
