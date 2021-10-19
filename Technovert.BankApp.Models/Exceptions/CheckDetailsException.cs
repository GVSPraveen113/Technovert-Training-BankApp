using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class CheckDetailsException:Exception
    {
        public CheckDetailsException()
        {
        }
        public override string Message
        {
            get
            {
                return "Please confirm your details before submitting";
            }
        }
    }
}
