using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface ITransactionService
    {
        public bool Deposit(string bankId, string accountId, string password, string currencyName, decimal deposit);
        public bool Withdraw(string bankId, string accountId, string password, decimal withdraw);
        public bool TransferMoney(string senderBankId, string senderActId, string password, string recieverBankId, string receiverActId, TransactionCharge transactionCharge, decimal amountTransfered);
        //public List<Transaction> GetTransactions(string bankName, string accountId, string password);
    }
}
