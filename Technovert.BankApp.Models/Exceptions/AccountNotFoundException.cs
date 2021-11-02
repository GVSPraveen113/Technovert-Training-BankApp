using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class AccountNotFoundException:Exception
    {
        public AccountNotFoundException()
        {
        }
        
        public AccountNotFoundException(string message): base(message) { }
    }
}
