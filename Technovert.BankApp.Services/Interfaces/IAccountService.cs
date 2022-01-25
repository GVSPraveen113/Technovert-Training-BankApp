using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface IAccountService
    {
        public string CreateAccount(string bankId, Account account/*string accountHolderName, string password, decimal initialDeposit, bool gender*/);
        public bool UpdateAccount(string bankId, string accountId, Account account);
        public bool DeleteAccount(string bankId, string accountId);
        public Account SingleAccount(Bank bank, string accountId);
        public List<Account> GetAllAccounts(string bankId);
    }
}
