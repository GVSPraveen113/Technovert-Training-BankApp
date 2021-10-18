using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int sourceAccountId { get; set; }
        public int DestinationaccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime On { get; set; }
    }
}
