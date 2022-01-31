using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technovert.BankApp.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public string AccountId { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 0 ;
        public List<Transaction> Transactions { get; set; }
        public string Name { get; set; }
        public bool isMale { get; set; }
        public AccountStatus Status { get; set; }
        public Bank Bank;
    }
}
