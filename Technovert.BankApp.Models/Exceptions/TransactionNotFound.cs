using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
        {
        }
        public TransactionNotFoundException(string message) : base(message) { }

    }
}
