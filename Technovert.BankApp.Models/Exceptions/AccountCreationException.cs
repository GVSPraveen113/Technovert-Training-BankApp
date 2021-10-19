using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    class AccountCreationException:Exception
    {
        public AccountCreationException()
        {
        }
        public override string Message
        {
            get
            {
                return "Try changing your Credentials. An Account is Existing with the above Credentials";
            }
        }
    }
}
