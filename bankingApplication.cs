using System;

namespace BankAccount
{
	public class Program
	{
		public class Account
		{
			public double balance;
			public Account(){ 
				balance=10000.00;
			}
			public double deposit(){
				double deposit;
				double NewBalance;
				Console.WriteLine("Enter Amount to Deposit");
				deposit=Double.Parse(Console.ReadLine());
				NewBalance=balance+deposit;
				return NewBalance;
			}
			public double withdraw(){
				double withdrawal;
				double NewBalance;
				Console.WriteLine("Enter Amount to Withdraw");
				withdrawal=Double.Parse(Console.ReadLine());
				NewBalance=balance-withdrawal;
				return NewBalance;
			}
		}
		public static void Main(String[] args)
		{ 
			double NewBalance;
			string balance;
			Account Bank1=new Account();
			
			Console.WriteLine("Your Current Balance is 10000");
			Console.WriteLine("Press d for deposit, w for withdraw");
			
			balance=Console.ReadLine();
			while(balance!="e"){
				if(balance=="d"){
					NewBalance=Bank1.deposit();
					Console.WriteLine("Your new balance is {0}",NewBalance);
				}
				if(balance=="w"){
					NewBalance=Bank1.withdraw();
					Console.WriteLine("Your new balance is {0}",NewBalance);
				}
				balance=Console.ReadLine();
			}
			Console.ReadLine();
		}
	}
}