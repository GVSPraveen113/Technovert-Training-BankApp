using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class BankNotFoundException:Exception
    {
        public BankNotFoundException()
        {
        }
        public override string Message
        {
            get
            {
                return "The Bank details provided are incorrect. Check details again";
            }
        }
    }
}
