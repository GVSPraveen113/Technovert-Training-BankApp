﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankService
    {
        private List<Bank> banks { get; set; }
        public BankService()
        {
            this.banks = new List<Bank>();
        }
        public string CreateBank(string name)
        {
            if (!CheckBankExistsByName(name))
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
                throw new BankCreationException("Bank Creation Failed! It seems Bank Already Exists");
            }
        }
        public string GetBankId(string name)
        {
            Bank bank=this.banks.Find(m => m.Name == name);
            if (bank == null)
            {
                throw new BankNotFoundException("Bank Id can't be retrieved. Please check whether this bank exists");
            }
            return bank.Id;
        }
        public bool CheckBankExistsByName(string name)
        {
            return this.banks.Any(b => b.Name == name);
        }
        public bool CheckBankExistsById(string id)
        {
            return this.banks.Any(b => b.Id == id);
        }
        public Bank SingleBank(string bankId)
        {
            Bank bank=this.banks.SingleOrDefault(m => m.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("The Bank details provided are incorrect. Check details again");
            }
            else
            {
                return bank;
            }
        }
        public bool AddNewCurrency(string bankId,string currencyName,decimal currencyValue)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.Id == bankId);
            if (bank.currenciesAccepted.ContainsKey(currencyName))
            {
                throw new BankNotFoundException("Given Currency already exists ! Sorry Operation Cannot be performed");
            }
            bank.currenciesAccepted.Add(currencyName, currencyValue);
            return true;
        }
        public bool AddServiceChargeSameBank(string bankId,decimal rtgs,decimal imps)
        {
            Bank bank = banks.SingleOrDefault(bank => bank.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSSameBank = rtgs;
            bank.IMPSSameBank = imps;
            return true;
        }
        public bool AddServiceChargeDiffBank(string bankId, decimal rtgs, decimal imps)
        {
            Bank bank = banks.SingleOrDefault(bank => bank.Id == bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSDiffBank = rtgs;
            bank.IMPSDiffBank = imps;
            return true;
        }

        private string GenerateRandomBankId(string bankName)
        {
            DateTime dt = new DateTime();
            string date = dt.ToShortDateString();
            if (bankName.Length < 3)
            {
                throw new IncorrectArgumentRangeException("Length must be greater than or equal to 3");
            }
            else 
            { 
                return bankName.Substring(0, 3) + date; 
            }

        }
    }
}
