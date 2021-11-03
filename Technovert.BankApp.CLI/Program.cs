using System;
using System.Collections.Generic;
using Technovert.BankApp.Services;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            BankService bankService = new BankService();
            AccountService accountService= new AccountService(bankService);
            TransactionService  transactionService= new TransactionService(bankService,accountService);
            ATMMessages.BankCreationMsg();
            string bankName = Inputs.GetBankName();
            string bankId=bankService.CreateBank(bankName);
            
            Console.WriteLine("Bank Id is "+bankId);
            bool isBankApplicationOpen = true;
            while (isBankApplicationOpen) {
                ATMMessages.SelectUserTypeMsg();
                UserType userType = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());
                if (userType == UserType.BankStaff)
                {
                    ATMMessages.DisplayBankStaffOptionsMsg();
                    bool exitVariable = true;
                    while (exitVariable) {
                        try {
                            BankStaffOptions staffOption = (BankStaffOptions)Enum.Parse(typeof(BankStaffOptions), Console.ReadLine());
                            switch (staffOption)
                            {
                                case BankStaffOptions.CreateAccount:
                                    try
                                    {
                                        ATMMessages.AccountCreationMsg();
                                        string bankIdentity = Inputs.GetBankId();
                                        string accountHolderName = Inputs.GetName();
                                        string password = Inputs.SetPassword();
                                        decimal amount = Inputs.GetInitialDeposit();
                                        bool gender = Inputs.SetGender();
                                        Console.WriteLine("Your account is created with Id " + accountService.CreateAccount(bankIdentity, accountHolderName, password, amount, gender));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.UpdateAccount:
                                    try
                                    {
                                        ATMMessages.UpdateAccountMsg();
                                        string bankIdentity = Inputs.GetBankId();
                                        string accountId = Inputs.GetAccountId();
                                        string updatedName = Inputs.GetName();
                                        bool updatedGender = Inputs.SetGender();
                                        if (accountService.UpdateAccount(bankIdentity, accountId, updatedName, updatedGender)){
                                            ATMMessages.SuccessMsg();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;
                                case BankStaffOptions.DeleteAccount:
                                    try
                                    {
                                        ATMMessages.ConfirmDeleteAccountMsg();
                                        string bankIdentity = Inputs.GetBankId();
                                        string accountId = Inputs.GetAccountId();
                                        if (accountService.DeleteAccount(bankIdentity, accountId))
                                            Console.WriteLine("Account Deleted Successfully");

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.AddNewCurrency:
                                    try
                                    {
                                        ATMMessages.AddNewCurrencyMsg();
                                        string bankIdentity = Inputs.GetBankId();
                                        string currencyName = Inputs.GetCurrencyName();
                                        decimal conversionToINR = Inputs.GetCurrencyValue();
                                        if (bankService.AddNewCurrency(bankIdentity,currencyName,conversionToINR))
                                        {
                                            ATMMessages.SuccessMsg();
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.AddServiceChargeSameBank:
                                    try
                                    {
                                        string bankid = Inputs.GetBankId();
                                        decimal rtgsCharges = Inputs.SetRtgsCharges();
                                        decimal impsCharges = Inputs.SetImpsCharges();
                                        if (bankService.AddServiceChargeSameBank(bankid, rtgsCharges, impsCharges))
                                        {
                                            ATMMessages.SuccessMsg();
                                        }
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.AddServiceChargeDiffBank:
                                    try
                                    {
                                        string bankid = Inputs.GetBankId();
                                        decimal rtgsCharges = Inputs.SetRtgsCharges();
                                        decimal impsCharges = Inputs.SetImpsCharges();
                                        if (bankService.AddServiceChargeSameBank(bankid, rtgsCharges, impsCharges))
                                        {
                                            ATMMessages.SuccessMsg();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.ViewAccountTransactionHistory:
                                    try
                                    {
                                        string BankName = Inputs.GetBankName();
                                        string AccountId = Inputs.GetAccountId();
                                        string password = Inputs.GetPassword();
                                        List<Transaction> transactions = transactionService.GetTransactions(BankName, AccountId, password);
                                        foreach (Transaction transaction in transactions)
                                        {
                                            Console.WriteLine(transaction.Id + " " + transaction.Type + " " + transaction.Amount + " " + transaction.On);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.RevertTransaction:
                                    try
                                    {
                                        string bankid = Inputs.GetBankId();
                                        string accountId = Inputs.GetAccountId();
                                        string transactionId = Inputs.GetTransactionId();
                                        if (transactionService.RevertTransaction(bankid, accountId, transactionId))
                                        {
                                            ATMMessages.SuccessMsg();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case BankStaffOptions.Exit:
                                    exitVariable = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid Selection");
                                    Console.ReadLine();
                                    break;
                            }
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Enter Valid Selection from the Menu");
                        }
                        
                    }  
                    
                }
                else if (userType == UserType.AccountHolder)
                {
                    ATMMessages.DisplayUserOptionsMsg();
                    bool exitVariable = true;
                    while (exitVariable)
                    {
                        try
                        {
                            AccountHolderOptions userOption = (AccountHolderOptions)Enum.Parse(typeof(AccountHolderOptions), Console.ReadLine());
                            switch (userOption)
                            {
                                
                                case AccountHolderOptions.Deposit:
                                    try
                                    {
                                        ATMMessages.AccountDetailsProvidingMsg();
                                        string bankid = Inputs.GetBankId();
                                        string accountId = Inputs.GetAccountId();
                                        string password = Inputs.GetPassword();
                                        IDictionary<string, decimal> currencies = new Dictionary<string,decimal>();
                                        currencies=bankService.FindCurrencies(bankid);
                                        ATMMessages.PrintAllCurrenciesAvailableMsg((Dictionary<string, decimal>)currencies);
                                        string currencyName = Inputs.GetCurrencyName();
                                        decimal amount = Inputs.GetDepositAmt();
                                        if (transactionService.Deposit(bankId, accountId, password,currencyName, amount))
                                        {
                                            ATMMessages.TransactionSuccessfulMsg();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case AccountHolderOptions.Withdraw:
                                    try
                                    {
                                        ATMMessages.AccountDetailsProvidingMsg();
                                        string bankname = Inputs.GetBankName();
                                        string accountId = Inputs.GetAccountId();
                                        string password = Inputs.GetPassword();
                                        decimal amount = Inputs.GetWithdrawAmt();
                                        if (transactionService.Withdraw(bankname, accountId,password, amount))
                                        {
                                            ATMMessages.TransactionSuccessfulMsg();
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;

                                case AccountHolderOptions.TransferMoney:
                                    try
                                    {
                                        string sourceBankName = Inputs.GetBankName();
                                        string senderAccountId = Inputs.GetAccountId();
                                        string password = Inputs.GetPassword();
                                        string receiverBankName = Inputs.GetBankName();
                                        string receiverAccountId = Inputs.GetRecepientAccountId();
                                        ATMMessages.DisplayTransactionChargesMsg();
                                        TransactionCharge transactioncharge = (TransactionCharge)Enum.Parse(typeof(TransactionCharge), Console.ReadLine());
                                        decimal amount = Inputs.GetTransferAmt();
                                        if (transactionService.TransferMoney(sourceBankName, senderAccountId,password, receiverBankName, receiverAccountId, transactioncharge, amount))
                                        {
                                            ATMMessages.TransactionSuccessfulMsg();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case AccountHolderOptions.ShowTransactions:
                                    try
                                    {
                                        string bankname = Inputs.GetBankName();
                                        string senderAccountId = Inputs.GetAccountId();
                                        string password = Inputs.GetPassword();
                                        List<Transaction> transactions = transactionService.GetTransactions(bankname, senderAccountId, password);
                                        foreach (Transaction transaction in transactions)
                                        {
                                            Console.WriteLine(transaction.Id + " " + transaction.Type + " " + transaction.Amount + " " + transaction.On);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case AccountHolderOptions.Exit:
                                    exitVariable = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid selection! Can't Proceed with this request");
                                    Console.ReadLine();
                                    break;

                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Enter valid Selection from the menu");
                        }
                    }
                    
                }
                else if(userType==UserType.ExitApplication)
                {
                    isBankApplicationOpen = false;
                }

            }
        }
    }
}


