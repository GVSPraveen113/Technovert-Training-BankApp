using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException()
        {
        }
       string Message
        {
            get
            {
                return "Please Deposit some amount. Transaction failed due to insufficient balance";
            }
        }
    }
}
