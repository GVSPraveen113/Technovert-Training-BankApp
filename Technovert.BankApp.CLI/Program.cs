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
            TransactionService  transactionService= new TransactionService(bankService);
            bool isBankApplicationOpen = true;
            while (isBankApplicationOpen) {
                ATMMessages.SelectUserTypeMsg();
                UserType userType = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());
                if (userType == UserType.BankStaff)
                {
                    bool exitVariable = true;
                    while (exitVariable) {
                        Console.WriteLine("Create your Bank");
                        string bankName = Console.ReadLine();
                        Console.WriteLine(bankService.CreateBank(bankName));
                        if (bankName == "5") {
                            exitVariable = false;
                        }
                        
                    }  
                    
                }
                else if (userType == UserType.AccountHolder)
                {
                    ATMMessages.DisplayOptionsMsg();
                    bool exitVariable = true;
                    while (exitVariable)
                    {
                        try
                        {
                            AccountHolderOptions userOption = (AccountHolderOptions)Enum.Parse(typeof(AccountHolderOptions), Console.ReadLine());
                            switch (userOption)
                            {
                                case AccountHolderOptions.CreateAccount:
                                    try
                                    {
                                        ATMMessages.AccountCreationMsg();
                                        string BankId = Inputs.GetBankId();
                                        string AccountHolderName = Inputs.GetName();
                                        string Password = Inputs.GetPassword();
                                        decimal Amount = Inputs.GetInitialDeposit();
                                        bool Gender = Inputs.SetGender();
                                        Console.WriteLine(accountService.CreateAccount(BankId, AccountHolderName, Password, Amount, Gender));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case AccountHolderOptions.Deposit:
                                    try
                                    {
                                        ATMMessages.AccountDetailsProvidingMsg();
                                        string BankName = Inputs.GetBankName();
                                        string AccountId = Inputs.GetAccountId();
                                        decimal Amount = Inputs.GetDepositAmt();
                                        if (transactionService.Deposit(BankName, AccountId, Amount))
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
                                        string BankName = Inputs.GetBankName();
                                        string AccountId = Inputs.GetAccountId();
                                        decimal Amount = Inputs.GetWithdrawAmt();
                                        if (transactionService.Withdraw(BankName, AccountId, Amount))
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
                                        string receiverBankName = Inputs.GetBankName();
                                        string receiverAccountId = Inputs.GetRecepientAccountId();
                                        ATMMessages.DisplayTransactionChargesMsg();
                                        TransactionCharge transactioncharge = (TransactionCharge)Enum.Parse(typeof(TransactionCharge), Console.ReadLine());
                                        decimal amount = Inputs.GetTransferAmt();
                                        if (transactionService.TransferMoney(sourceBankName, senderAccountId, receiverBankName, receiverAccountId, transactioncharge, amount))
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
                                        string BankName = Inputs.GetBankName();
                                        string senderAccountId = Inputs.GetAccountId();
                                        List<Transaction> transactions = transactionService.GetTransactions(BankName, senderAccountId);
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
                                /*
                            case UserOptions.ShowTransactions:
                                ATM.ShowTransactions();
                                break;
                            case UserOptions.Exit:
                                ATM.ExitApplication();
                                return;
                            case UserOptions.ViewAllAccounts:
                                string username = Inputs.GetAccountName();
                                Console.WriteLine(ATM.ViewAllAccounts(username));
                                break;
                            */
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


