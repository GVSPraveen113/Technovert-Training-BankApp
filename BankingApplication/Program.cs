using System;
using System.Collections.Generic;


namespace Bank{
    public class Program{
        public static void Main(string[] args){

            ATMMessages.displayOptions();
            while(true){
                try{
                int selection=int.Parse(Console.ReadLine());
                switch(selection){
                    case 1:
                        ATMFunctionality.CreateAccount();
                        break;
                    case 2:
                        ATMFunctionality.Deposit();
                        break;
                    case 3:
                        ATMFunctionality.Withdraw();
                        break;
                    case 4:
                        ATMFunctionality.TransferMoney();
                        break;
                    case 5:
                        ATMFunctionality.ShowTransactions();
                        break;
                    case 6:
                        ATMFunctionality.ExitApplication();
                        return; 
                    case 7:
                        ATMFunctionality.ViewAllAccounts();  
                        break;    
                    default:
                        Console.WriteLine("Invalid selection! Can't Proceed with this request");
                        Console.ReadLine(); 
                        break;  
                 }
                }
                catch{
                    ATMMessages.ExceptionMsg();
                }
            }

        }
    }
}