using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Technovert.BankApp.Models;
using System.Threading.Tasks;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface IBankService
    {
        public string CreateBank(Bank bankDTO);
        public Bank SingleBank(string bankId);
        public bool UpdateBank(string bankId, Bank bankDTO);
        public bool DeleteBank(string bankId);
        public string GetBankId(string name);
        public List<Bank> GetAllBanks();
        public bool AddNewCurrency(string bankId, string currencyName, decimal currencyValue);
        public bool AddServiceChargeSameBank(string bankId, decimal rtgs, decimal imps);
        public bool AddServiceChargeDiffBank(string bankId, decimal rtgs, decimal imps);
    }
}
