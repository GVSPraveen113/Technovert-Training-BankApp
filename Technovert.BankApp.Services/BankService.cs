using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using MySql.Data.MySqlClient;

namespace Technovert.BankApp.Services
{
    public class BankService: IBankService
    {
        /*string cs = @"server=localhost;userid=praveengvs;password=1234;database=banksdb";
        using var con = new MySqlConnection(cs);
        con.Open();*/
        BankDbContext bankDb = new BankDbContext();
        public List<Bank> banks { get; set; }

        string jsonBanks = Path.Combine(Directory.GetCurrentDirectory(), @"..\banks.json");
        public BankService()
        {

        }
        public string CreateBank(Bank bankDTO)
        {
            if (!CheckBankExistsByName(bankDTO.Name))
            {
                Bank bank = new Bank
                {
                    BankId = this.GenerateRandomBankId(bankDTO.Name),
                    Name = bankDTO.Name,
                    Accounts = new List<Account>(),
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                bankDb.Banks.Add(bank);
                bankDb.SaveChanges();

                /*banks.Add(bank);
                string json = JsonSerializer.Serialize(banks);
                File.WriteAllText(jsonBanks, json);*/
                return bank.BankId;
            }
            else
            {
                throw new BankCreationException("Bank Creation Failed! It seems Bank Already Exists");
            }
        }
        public IDictionary<string, decimal> FindCurrencies(string bankId)
        {
            List<Bank> banksList = bankDb.Banks.ToList();
            Bank bank = banksList.Find(m => m.BankId == bankId);
            return bank.CurrenciesAccepted;
        }
        public string GetBankId(string name)
        {
            List<Bank> banksList = bankDb.Banks.ToList();
            Bank bank = banksList.Find(m => m.Name == name);
            if (bank == null)
            {
                throw new BankNotFoundException("Bank Id can't be retrieved. Please check whether this bank exists");
            }
            return bank.BankId;
        }
        public bool CheckBankExistsByName(string name)
        {
            /*string jsonstring = File.ReadAllText(jsonBanks);
            List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);*/
            List<Bank> banksList = bankDb.Banks.ToList();
            return banksList.Any(b => b.Name == name);
        }
        public bool CheckBankExistsById(string id)
        {
            List<Bank> banksList = bankDb.Banks.ToList();
            return banksList.Any(b => b.BankId == id);
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
        public bool AddNewCurrency(string bankId, string currencyName, decimal currencyValue)
        {
            Bank bank = SingleBank(bankId);
            if (bank.CurrenciesAccepted.ContainsKey(currencyName))
            {
                throw new BankNotFoundException("Given Currency already exists ! Sorry Operation Cannot be performed");
            }
            bank.CurrenciesAccepted.Add(currencyName, currencyValue);
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            bankDb.SaveChanges();
            return true;

        }
        public bool AddServiceChargeSameBank(string bankId, decimal rtgs, decimal imps)//same method
        {
            Bank bank = SingleBank(bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSSameBank = rtgs;
            bank.IMPSSameBank = imps;
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            bankDb.SaveChanges();
            return true;
        }
        public bool AddServiceChargeDiffBank(string bankId, decimal rtgs, decimal imps)
        {
            Bank bank = SingleBank(bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("No Bank is found with the given ID");
            }
            bank.RTGSDiffBank = rtgs;
            bank.IMPSDiffBank = imps;
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            bankDb.SaveChanges();
            return true;
        }

        private string GenerateRandomBankId(string bankName)
        {
            string dateTime = DateTime.Now.ToString("ddmmyyyy");
            if (bankName.Length < 3)
            {
                throw new IncorrectArgumentRangeException("Length must be greater than or equal to 3");
            }
            else
            {
                return bankName.Substring(0, 3) + dateTime;
            }

        }
        public List<Bank> GetAllBanks()
        {
            return bankDb.Banks.Include(b => b.Accounts).ToList();
        }
        public bool UpdateBank(string bankId,Bank bankDTO)
        {
            Bank bank = SingleBank(bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("Account isn't found!");
            }
            bank.Name = bankDTO.Name;
            bank.UpdatedBy = "Admin";
            bank.UpdatedOn = DateTime.Now;
            bankDb.SaveChanges();
            return true;
        }
        public bool DeleteBank(string bankId)
        {
            Bank bank = SingleBank(bankId);
            if (bank == null)
            {
                throw new BankNotFoundException("Account isn't found!");
            }
            bankDb.Banks.Remove(bank);
            bankDb.SaveChanges();
            return true;
        }
        public bool retreiveJson()
        {
            string jsonstring = File.ReadAllText(jsonBanks);
            this.banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
            return true;
        }
        public bool saveJson()
        {
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            return true;
        }
        public bool ExitApplication()
        {
            string json = JsonSerializer.Serialize(banks);
            File.WriteAllText(jsonBanks, json);
            bankDb.SaveChanges();
            return true;
        }
    }

}