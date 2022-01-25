using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technovert.BankApp.API.DTOs.Account
{
    public class CreateAccountDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; } = 0;
        public bool isMale { get; set; }
    }
}
