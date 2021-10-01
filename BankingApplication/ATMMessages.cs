using System;
namespace Bank{
    public class ATMMessages{
        public static void displayOptions(){
            Console.WriteLine("Press 1. for Account Creation");
            Console.WriteLine("Press 2. for Deposit");
            Console.WriteLine("Press 3. for Withdrawal");
            Console.WriteLine("Press 4. for Transfer Money");
            Console.WriteLine("Press 5. for Show Transactions");
        }
    }
}