using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class AccountNotFoundException:Exception
    {
        public AccountNotFoundException()
        {
        }
        public override string Message
        {
            get
            {
                return "Account is not Found in the Database. Please recheck your credentials or create a new account";
            }
        }
    }
}
