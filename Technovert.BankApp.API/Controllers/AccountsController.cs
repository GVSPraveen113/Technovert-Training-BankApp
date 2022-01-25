using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Services.Interfaces;
using Technovert.BankApp.API.DTOs.Account;
using Technovert.BankApp.Models;
using AutoMapper;
using Technovert.BankApp.Services;

namespace Technovert.BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpGet("{bankId}")]
        public IActionResult GetAllAccounts(string bankId)
        {
            return Ok(accountService.GetAllAccounts(bankId));
        }
        [HttpPost("{bankId}")]
        public IActionResult CreateAccount(string bankId, [FromBody] CreateAccountDTO accountDTO)
        {
            Account account = mapper.Map<Account>(accountDTO);
            return Ok(accountService.CreateAccount(bankId,account));
        }
        [HttpPut("{bankId}/{accountId}")]
        public IActionResult UpdateAccount(string bankId, string accountId, [FromBody] UpdateAccountDTO accountDTO)
        {
            Account account = mapper.Map<Account>(accountDTO);
            return Ok(accountService.UpdateAccount(bankId, accountId, account));
        }
        [HttpDelete("{bankId}/{accountId}")]
        public IActionResult DeleteAccount(string bankId,string accountId)
        {
            return Ok(accountService.DeleteAccount(bankId, accountId));
        }
        

    }
}
