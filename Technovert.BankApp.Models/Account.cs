using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 0 ;
        public List<Transaction> Transactions { get; set; }
        public string Name { get; set; }
        public bool isMale { get; set; }
        public AccountStatus Status { get; set; }

        //Navigation property
        //public string? BankId { get; set; }
    }
}
