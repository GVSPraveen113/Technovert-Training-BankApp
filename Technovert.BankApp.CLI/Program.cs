﻿using System;
using System.Collections.Generic;
using Technovert.BankApp.Services;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            BankService bs = new BankService();
            //AccountService as= new AccountService();
            TransactionService ts = new TransactionService();
            Console.WriteLine("Create your Bank");
            Console.WriteLine(bs.CreateBank(Console.ReadLine()));
            ATMMessages.DisplayOptionsMsg();
            while (true)
            {
                UserOptions userOption = (UserOptions)Enum.Parse(typeof(UserOptions), Console.ReadLine());
            
                    switch (userOption)
                    {
                        case UserOptions.CreateAccount:
                            try
                            {
                                ATMMessages.AccountCreationMsg();
                                string BankId = Inputs.GetBankId();
                                string AccountHolderName = Inputs.GetName();
                                string Password = Inputs.GetPassword();
                                decimal Amount = Inputs.GetInitialDeposit();
                                bool Gender = Inputs.SetGender();
                                Console.WriteLine(bs.CreateAccount(BankId, AccountHolderName, Password, Amount, Gender));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case UserOptions.Deposit:
                            try
                            {
                                ATMMessages.AccountDetailsProvidingMsg();
                                string BankName = Inputs.GetBankName();
                                string AccountId = Inputs.GetAccountId();
                                decimal Amount = Inputs.GetDepositAmt();
                                if(ts.Deposit(bs,BankName, AccountId, Amount))
                                {
                                ATMMessages.TransactionSuccessfulMsg();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case UserOptions.Withdraw:
                            try
                            {
                                ATMMessages.AccountDetailsProvidingMsg();
                                string BankName = Inputs.GetBankName();
                                string AccountId = Inputs.GetAccountId();
                                decimal Amount = Inputs.GetWithdrawAmt();
                                if(ts.Withdraw(bs,BankName, AccountId, Amount))
                                {
                                    ATMMessages.TransactionSuccessfulMsg();
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;

                        case UserOptions.TransferMoney:
                            try
                            {
                                string sourceBankName = Inputs.GetBankName();
                                string senderAccountId = Inputs.GetAccountId();
                                string receiverBankName = Inputs.GetBankName();
                                string receiverAccountId = Inputs.GetRecepientAccountName();
                                decimal amount = Inputs.GetTransferAmt();
                                if(ts.TransferMoney(bs,sourceBankName, senderAccountId, receiverBankName, receiverAccountId, amount))
                                {
                                    ATMMessages.TransactionSuccessfulMsg();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                    case UserOptions.ShowTransactions:
                        try
                        {
                            string BankName = Inputs.GetBankName();
                            string senderAccountId = Inputs.GetAccountId();
                            List<Transaction> transactions=ts.GetTransactions(bs,BankName, senderAccountId);
                            foreach (Transaction transaction in transactions)
                            {
                                Console.WriteLine(transaction.Id + " " + transaction.Type + " " +transaction.Amount + " " + transaction.On);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
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
        }
    }
}

public enum UserOptions
{
    CreateAccount = 1,
    Deposit,
    Withdraw,
    TransferMoney,
    ShowTransactions,
    Exit,
    ViewAllAccounts
}
