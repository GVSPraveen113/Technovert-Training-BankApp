using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class BankCreationException:Exception
    {
        public BankCreationException()
        {
        }
        public BankCreationException(string message) : base(message) { }
        
    }
}
