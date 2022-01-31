using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technovert.BankApp.API.DTOs.Transaction
{
    public class CreateTransferDTO
    {
        public decimal Amount { get; set; }
        public string DestinationAccountId { get; set; }
    }
}
