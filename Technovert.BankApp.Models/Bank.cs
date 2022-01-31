using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technovert.BankApp.Models
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        public string BankId { get; set; }
        public string Name { get; set;}
        public ICollection<Account> Accounts { get; set; }

        public IDictionary<string, decimal> CurrenciesAccepted = new Dictionary<string, decimal>();
        public DateTime? CreatedOn { get; set; } //Audit Properties
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Charges charges { get; set; }
        public decimal RTGSSameBank { get; set; } = 0;
        public decimal RTGSDiffBank { get; set; } = 2;
        public decimal IMPSSameBank { get; set; } = 5;
        public decimal IMPSDiffBank { get; set; } = 6;
        public CurrencyType currency { get; set; } = CurrencyType.INR;
    }
}
