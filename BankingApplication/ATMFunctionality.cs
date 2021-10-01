using System;
namespace Bank{
    public class ATMFunctionality{
        public static double CreateAccount(){
            double amount=double.Parse(Console.ReadLine());
            return amount;
        }

        public static double Deposit(double balance){
            double creditAmt=double.Parse(Console.ReadLine());
            balance+=creditAmt;
            return balance;
        }
        public static double Withdraw(double balance){
            double debitAmt=double.Parse(Console.ReadLine());
            balance-=debitAmt;
            return balance;
        }
        public static void TransferMoney(){

        }
        public static void ShowTransactions(){
        
        }
        
    }
}