using System;
using System.Collections.Generic;

namespace Bank{
    public class ATMFunctionality{
        static IDictionary<string,double> bankaccounts = new Dictionary<string,double>();
        public static void CreateAccount(){
            ATMMessages.accountCreationMessage();
            try{
            string name=Console.ReadLine();
            double amount=double.Parse(Console.ReadLine());
            bankaccounts.Add(name,amount);
            Console.WriteLine("You have deposited INR: {0}",bankaccounts[name]);
            }
            catch{
                ATMMessages.ExceptionMsg();
            }
        }

        public static void Deposit(){
            try{
            ATMMessages.AccountDetailsProvidingMsg();
            string account=Console.ReadLine();
            if(bankaccounts.ContainsKey(account)){  
                ATMMessages.DepositAmtMsg();
                double depositAmount=double.Parse(Console.ReadLine());
                bankaccounts[account]+=depositAmount;
                Console.WriteLine("The balance is {0}",bankaccounts[account]);
            }
            }
            catch{
                ATMMessages.ExceptionMsg();
            }
        }
        public static void Withdraw(){
            try{
            ATMMessages.AccountDetailsProvidingMsg();
            string account=Console.ReadLine();
            if(bankaccounts.ContainsKey(account)){  
                ATMMessages.WithdrawAmtMsg();
                double withdrawAmount=double.Parse(Console.ReadLine());
                if(withdrawAmount<bankaccounts[account]){
                    bankaccounts[account]-=withdrawAmount;
                }
                else{
                    ATMMessages.InsufficientBalanceMsg();
                }
                Console.WriteLine("The balance is {0}",bankaccounts[account]);
            }
            }
            catch{
                ATMMessages.ExceptionMsg();
            }
            
        }
        public static void TransferMoney(){
            try{
            ATMMessages.AccountDetailsProvidingMsg();
            string FromAccount=Console.ReadLine();
            Console.WriteLine("Enter account details of reciever");
            string ToAccount=Console.ReadLine();
            Console.WriteLine("Enter amount you wish to transfer");
            double TransferAmt=double.Parse(Console.ReadLine());
            if(TransferAmt<bankaccounts[FromAccount]){
                    bankaccounts[FromAccount]-=TransferAmt;
                    bankaccounts[ToAccount]+=TransferAmt;
                    Console.WriteLine("Tranfer from {0} to {1} of INR {2} successful ",FromAccount,ToAccount,TransferAmt);
                    Console.WriteLine("The balance is {0}",bankaccounts[FromAccount]);
            }
            else{
                    ATMMessages.TransferFailedMsg();
            }
            }
            catch{
                ATMMessages.ExceptionMsg();
            }
        }

        public static void ShowTransactions(){
        
        }
        public static void ViewAllAccounts(){
            Console.WriteLine("Enter your username");
            string username=Console.ReadLine();
            if(username=="admin"){
                foreach(var kvp in bankaccounts)
                    Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
                }
            else{
                Console.WriteLine("Access not granted!!");
            }
        }
         public static void ExitApplication(){

         }
    }
}