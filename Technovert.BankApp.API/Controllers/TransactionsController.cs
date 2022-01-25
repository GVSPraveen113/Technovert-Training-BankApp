using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Services.Interfaces;
using AutoMapper;

namespace Technovert.BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IMapper mapper;
        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            this.transactionService = transactionService;
            this.mapper = mapper;
        }
        [HttpGet("{bankId}")]
        public IActionResult GetTransactionsOfBank()
        {
            return NotFound();
        }
        [HttpGet("{bankId}/{accountId}")]
        public IActionResult GetTransactionsOfAccount()
        {
            return NotFound();
        }
        [HttpPost("{operationType}")]
        public IActionResult ATMOperations()
        {
            return NoContent();
        }
    }
}