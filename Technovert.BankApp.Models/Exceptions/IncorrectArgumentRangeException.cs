using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class IncorrectArgumentRangeException:Exception
    {
        public IncorrectArgumentRangeException()
        {
        }
        public override string Message
        {
            get
            {
                return "Length must be greater than or equal to 3";
            }
        }
    }
}
