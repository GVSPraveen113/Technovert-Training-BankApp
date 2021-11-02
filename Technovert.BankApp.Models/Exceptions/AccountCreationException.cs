using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    class AccountCreationException:Exception
    {
        public AccountCreationException()
        {
        }
        public AccountCreationException(string message) : base(message) { }
        

    }
}
