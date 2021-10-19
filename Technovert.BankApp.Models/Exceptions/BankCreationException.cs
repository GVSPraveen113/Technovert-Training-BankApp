using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class BankCreationException:Exception
    {
        public BankCreationException()
        {
        }
        public override string Message
        {
            get
            {
                return "Bank Already Exists with these details";
            }
        }
    }
}
