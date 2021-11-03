using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.CLI
{
    public static class ATMMessages
    {
        public static void SelectUserTypeMsg()
        {
            Console.WriteLine("Select 1 if you are BankStaff and Select 2 if you are Account Holder");
        }
        public static void BankCreationMsg()
        {
            Console.WriteLine("Enter Bank Name to Create a Bank");
        }
        public static void UpdateAccountMsg()
        {
            Console.WriteLine("Please Enter your name and gender 'true if male and false if female' to update these details in your account");
        }
        public static void DisplayUserOptionsMsg()
        {
            Console.WriteLine("Press 1. for Deposit");
            Console.WriteLine("Press 2. for Withdrawal");
            Console.WriteLine("Press 3. for Transfer Money");
            Console.WriteLine("Press 4. for Show Transactions");
            Console.WriteLine("Press 5. to Exit ATM Application");
        }
        public static void DisplayBankStaffOptionsMsg()
        {
            Console.WriteLine("Press 1. for Creating Account");
            Console.WriteLine("Press 2. for Updating Account");
            Console.WriteLine("Press 3. for Deleting Account");
            Console.WriteLine("Press 4. for Adding New Currency");
            Console.WriteLine("Press 5. for Adding Service Charge Same Bank,");
            Console.WriteLine("Press 6. for Adding ServiceCharge Diff Bank");
            Console.WriteLine("Press 7. for Viewing Account Transaction History");
            Console.WriteLine("Press 8. for Reverting Transaction");
            Console.WriteLine("Press 9. for returning to Main Menu");
        }
        public static void AccountCreationMsg()
        {
            Console.WriteLine("-----Account Creation-----");
            Console.WriteLine("Enter your Name and Initial Deposit");
        }
        public static void AccountDetailsProvidingMsg()
        {
            Console.WriteLine("Enter your account details");
        }
        public static void RecieverAccountDetailsProvidingMsg()
        {
            Console.WriteLine("Enter the Account Details of Reciever");
        }
        public static void DepositAmtMsg()
        {
            Console.WriteLine("Enter amount to be deposited");
        }
        public static void WithdrawAmtMsg()
        {
            Console.WriteLine("Enter amount to be withdrawn");
        }
        public static void InsufficientBalanceMsg()
        {
            Console.WriteLine("You have insufficient balance. Please deposit some amount first");
        }
        public static void TransferFailedMsg()
        {
            Console.WriteLine("Transfer failed due to insufficient balance");
        }
        public static void ExceptionMsg()
        {
            Console.WriteLine("You have entered some of the details incorrectly. Please check back and proceed");
        }
        public static void TransactionSuccessfulMsg()
        {
            Console.WriteLine("The Transaction performed is successful");
        }
        public static void DisplayTransactionChargesMsg()
        {
            Console.WriteLine("Select Method of Transaction. Press 1 for RTPS, 2 for IMPS\n RTPS Same Bank - 0%, RTPS Different Bank - 2%\n IMPS Same Bank - 5%, IMPS Different Bank - 6%");
        }
        public static void ConfirmDeleteAccountMsg()
        {
            Console.WriteLine("Are you Sure to delete the Account");
        }
        public static void SuccessMsg()
        {
            Console.WriteLine("Successfully Performed the operation!");
        }
        public static void AddNewCurrencyMsg()
        {
            Console.WriteLine("Add New Currency!!");
        }
    }
}
