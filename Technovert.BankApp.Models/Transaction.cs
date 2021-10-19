using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string sourceAccountId { get; set; }
        public string DestinationaccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime On { get; set; }
    }
}
