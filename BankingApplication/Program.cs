using System;
using System.Collections.Generic;


namespace Bank{
    public class Program{
        public static void Main(string[] args){

            ATMMessages.displayOptions();
            double balance=ATMFunctionality.CreateAccount();;
            while(true){
                int selection=int.Parse(Console.ReadLine());
                double retamt;
                switch(selection){
                    case 1:
                        Console.WriteLine("The initial amount is {0}",balance);
                        break;
                    case 2:
                        retamt=ATMFunctionality.Deposit(balance);
                        balance=retamt;
                        Console.WriteLine("The remaining amount after transaction is {0}",balance);
                        break;
                    case 3:
                        retamt=ATMFunctionality.Withdraw(balance);
                        balance=retamt;
                        Console.WriteLine("The remaining amount after transaction is {0}",balance);
                        break;
                    case 4:
                        ATMFunctionality.TransferMoney();
                        break;
                    case 5:
                        ATMFunctionality.ShowTransactions();
                        break;
                    default:
                        Console.WriteLine("Invalid selection! Can't Proceed with this request");
                        Console.ReadLine(); 
                        break;  
                }
            }

        }
    }
}