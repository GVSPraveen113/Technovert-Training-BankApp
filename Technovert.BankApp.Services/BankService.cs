using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankService
    {
        private List<Bank> banks { get; set; }
        string jsonBanks = "F:/Visual Studio Code Projects/Technovert.BankApp/banks.json";
        //F:\Visual Studio Code Projects\Technovert.BankApp\banks.json
        public BankService()
        {
            /*FileStream banksJson = new FileStream("F:/Visual Studio Code Projects/Technovert.BankApp/banks.json", FileMode.OpenOrCreate);
            banksJson.Close();*/

            
            /*try
            {
                
                var jsonString = File.ReadAllText(fileName);
                this.banks = JsonSerializer.Deserialize<List<Bank>>(jsonString);
            }
            catch(FileNotFoundException)
            {
                File.Create("F:/Visual Studio Code Projects/Technovert.BankApp/banks.json");
            }
            catch (JsonException)
            {
                this.banks = new List<Bank>();
            }
            /*finally
            {
                fileName.Close();
            }*/
            this.banks = new List<Bank>();
            
        }
        public string CreateBank(string name)
        {
            /*if(!File.Exists("F:/Visual Studio Code Projects/Technovert.BankApp/banks.json"))
            {
                File.Create("F:/Visual Studio Code Projects/Technovert.BankApp/banks.json");
            }
            var banksJson=File.ReadAllText("F:/Visual Studio Code Projects/Technovert.BankApp/banks.json");
            try 
            {
                
            }
            catch (JsonException)
            {
                this.banks = new List<Bank>();
            }
            */
            if (!CheckBankExistsByName(name))
            {
                
                Bank bank = new Bank
                {
                    Id = this.GenerateRandomBankId(name),
                    Name = name,
                    Accounts = new List<Account>()
                };
                //banks.Add(bank);
                string jsonstring = File.ReadAllText(jsonBanks);
                List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
                banks.Add(bank);
                string json = JsonSerializer.Serialize(banks);
                File.WriteAllText(jsonBanks, json);
                return bank.Id;
            }
            else
            {
                throw new BankCreationException("Bank Creation Failed! It seems Bank Already Exists");
            }
        }
        public IDictionary<string,decimal> FindCurrencies(string bankId)
        {
            Bank bank= this.banks.Find(m => m.Id == bankId);
            return bank.CurrenciesAccepted;
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
            string jsonstring = File.ReadAllText(jsonBanks);
            List<Bank> banks = JsonSerializer.Deserialize<List<Bank>>(jsonstring);
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
            Bank bank = SingleBank(bankId);
            if (bank.CurrenciesAccepted.ContainsKey(currencyName))
            {
                throw new BankNotFoundException("Given Currency already exists ! Sorry Operation Cannot be performed");
            }
            bank.CurrenciesAccepted.Add(currencyName, currencyValue);
            return true;
        }
        public bool AddServiceChargeSameBank(string bankId,decimal rtgs,decimal imps)//same method
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
        public bool ExitApplication()
        {
            /*string jsonBanks = JsonSerializer.Serialize(banks);
            File.WriteAllText(@"F:\Visual Studio Code Projects\Technovert.BankApp",jsonBanks);*/     
            return true;
        }
    }
   
}