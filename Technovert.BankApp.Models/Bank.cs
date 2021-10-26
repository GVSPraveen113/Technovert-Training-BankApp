using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Bank
    {
        public string Id { get; set; }
        public string Name { get; set;}
        public List<Account> Accounts{ get; set; }
        public DateTime CreatedOn { get; set; } //Audit Properties
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Charges charges { get; set; }
        public decimal RTGSSameBank { get; set; } = 0;
        public decimal RTGSDiffBank { get; set; } = 2;
        public decimal IMPSSameBank { get; set; } = 5;
        public decimal IMPSDiffBank { get; set; } = 6;
        public CurrencyType currency { get; set; } = CurrencyType.INR;
    }
}
