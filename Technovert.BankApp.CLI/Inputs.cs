﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.CLI
{
    public static class Inputs
    {
        public static string GetName()
        {
            Console.WriteLine("Please Enter Your Name:");
            return Console.ReadLine();
        }
        public static string GetAccountId()
        {
            Console.WriteLine("Please Enter Your AccountId:");
            return Console.ReadLine();
        }
        public static decimal GetInitialDeposit()
        {
            Console.WriteLine("Please Enter the Money You want to Deposit:");
            return decimal.Parse(Console.ReadLine());
        }
        public static decimal GetDepositAmt()
        {
            Console.WriteLine("Please Enter the Money You want to Deposit:");
            return decimal.Parse(Console.ReadLine());
        }
        public static decimal GetWithdrawAmt()
        {
            Console.WriteLine("Please Enter the Money You want to Withdraw:");
            return decimal.Parse(Console.ReadLine());
        }
        public static decimal GetTransferAmt()
        {
            Console.WriteLine("Please Enter the Money You want to Transfer:");
            return decimal.Parse(Console.ReadLine());
        }

        public static string GetRecepientAccountName()
        {
            Console.WriteLine("Please Enter the Recipient Name:");
            return Console.ReadLine();
        }
        public static string GetPassword()
        {
            Console.WriteLine("Set a Password for your Account:");
            return Console.ReadLine();
        }
        public static bool SetGender()
        {
            Console.WriteLine("Enter true for Male, false for Female:");
            return bool.Parse(Console.ReadLine());
        }
        public static string GetBankId()
        {
            Console.WriteLine("Enter Bank Id");
            return Console.ReadLine();
        }
        public static string GetBankName()
        {
            Console.WriteLine("Enter Bank Name");
            return Console.ReadLine();
        }

    }
}