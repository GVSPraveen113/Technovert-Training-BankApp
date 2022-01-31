using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface ITransactionService
    {
        public string Deposit(string bankId, string accountId, Transaction transactionDTO);
        public string Withdraw(string bankId, string accountId, Transaction transactionDTO);
        public string TransferMoney(string sourceBankId, string sourceActId, string destinationBankId, TransactionCharge transactionCharge, Transaction transactionDTO);
        public List<Transaction> GetTransactionsOfAccount(string accountId);
        public List<Transaction> GetAllTransactions();


    }
}
