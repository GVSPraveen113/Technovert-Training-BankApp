using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technovert.BankApp.API.DTOs.Account
{
    public class UpdateAccountDTO
    {
        public string Name { get; set; }
        public bool isMale { get; set; }
    }
}
