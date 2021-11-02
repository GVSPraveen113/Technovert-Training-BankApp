using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class IncorrectArgumentRangeException:Exception
    {
        public IncorrectArgumentRangeException()
        {
        }
        public IncorrectArgumentRangeException(string message) : base(message) { }
        
    }
}
