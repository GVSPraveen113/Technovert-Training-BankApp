using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Services.Interfaces;
using Technovert.BankApp.API.DTOs.Transaction;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
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
        [HttpGet]
        public IActionResult GetTransactionsOfBank()
        {
            return Ok(transactionService.GetAllTransactions());
        }
        [HttpGet("{accountId}")]
        public IActionResult GetTransactionsOfAccount(string accountId)
        {
            return Ok(transactionService.GetTransactionsOfAccount(accountId));
        }
        [HttpPost("{bankId}/{accountId}/deposit")]
        public IActionResult Deposit(string bankId,string accountId,[FromBody] CreateDepositDTO transactionDTO)
        {
            Transaction transaction = mapper.Map<Transaction>(transactionDTO);
            return Ok(transactionService.Deposit(bankId,accountId,transaction));
        }
        [HttpPost("{bankId}/{accountId}/withdraw")]
        public IActionResult Withdraw(string bankId, string accountId, [FromBody] CreateDepositDTO transactionDTO)
        {
            Transaction transaction = mapper.Map<Transaction>(transactionDTO);
            return Ok(transactionService.Withdraw(bankId, accountId, transaction));
        }
        [HttpPost("{sourceBankId}/{sourceAccountId}/transfer/{receiverBankId}/{transactionCharge}")]
        public IActionResult Transfer(string sourceBankId, string sourceAccountId, [FromRoute] string receiverBankId, [FromRoute] TransactionCharge transactionCharge, [FromBody] CreateTransferDTO transactionDTO )
        {
            Transaction transaction = mapper.Map<Transaction>(transactionDTO);
            return Ok(transactionService.TransferMoney(sourceBankId, sourceAccountId, receiverBankId, transactionCharge, transaction));
        }
    }
}