using System;
using Technovert.BankApp.Services;

namespace Technovert.BankApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            BankServices bs = new BankServices();
            Console.WriteLine(bs.CreateBank("Technovert Bank"));
           
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
                            Console.WriteLine(bs.CreateAccount(BankId,AccountHolderName,Password,Amount,Gender));
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
                            string BankName= Inputs.GetBankName();
                            string AccountId= Inputs.GetAccountId();
                            decimal Amount = Inputs.GetDepositAmt();
                            Console.WriteLine(bs.Deposit(BankName,AccountId,Amount));
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
                            Console.WriteLine(bs.Withdraw(BankName, AccountId, Amount));
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
                            Console.WriteLine(bs.TransferMoney(sourceBankName,senderAccountId,receiverBankName, receiverAccountId,amount));
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
