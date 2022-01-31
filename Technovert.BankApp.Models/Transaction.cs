using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technovert.BankApp.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public Account SourceAccount { get; set; }
        public Account DestinationAccount { get; set; }
        public TransactionMode Mode { get; set; }
        public TransactionType Type { get; set; }
        public DateTime On { get; set; }
    }
}
