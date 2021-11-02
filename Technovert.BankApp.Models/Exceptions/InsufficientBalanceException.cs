using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException()
        {
        }
        public InsufficientBalanceException(string message) : base(message) { }
       
    }
}
