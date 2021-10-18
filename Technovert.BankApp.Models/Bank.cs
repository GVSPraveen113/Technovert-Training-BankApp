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
    }
}
