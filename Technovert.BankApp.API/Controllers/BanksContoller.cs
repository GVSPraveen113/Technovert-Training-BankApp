using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Technovert.BankApp.Models;
using Technovert.BankApp.Services;
using Technovert.BankApp.Services.Interfaces;
using Technovert.BankApp.API.DTOs.Bank;
using System.Threading.Tasks;
using AutoMapper;
using Technovert.BankApp.API.Profiles;

namespace Technovert.BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : Controller
    {
        private readonly IBankService bankService;
        private readonly IMapper mapper;
        public BanksController(IBankService bankService,IMapper mapper)
        {
            this.bankService = bankService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBanks()
        {
            return Ok(bankService.GetAllBanks());
        }
        [HttpGet("{bankId}")]
        public IActionResult GetBank([FromRoute] string bankId)
        {
            Bank bank = bankService.SingleBank(bankId);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(bank);
        }

        [HttpPost]
        public IActionResult CreateBank([FromBody] CreateBankDTO bankDTO)
        { 
            Bank bank = mapper.Map<Bank>(bankDTO);
            return Ok(bankService.CreateBank(bank));
        }

        [HttpPut("{bankId}")]
        public IActionResult UpdateBank(string bankId,[FromBody] CreateBankDTO bankDTO)
        {
            Bank bank = mapper.Map<Bank>(bankDTO);
            return Ok(bankService.UpdateBank(bankId,bank));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBank(string id)
        {
            try
            {
                bool deletedBank = bankService.DeleteBank(id);
                if (deletedBank)
                {
                    return Ok("The Bank is Deleted successfully");
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
