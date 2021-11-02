using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class CheckDetailsException:Exception
    {
        public CheckDetailsException()
        {
        }
        public CheckDetailsException(string message) : base(message) { }
        
    }
}
