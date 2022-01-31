using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Technovert.BankApp.API.DTOs.Transaction;
using Technovert.BankApp.API.DTOs.Account;
using Technovert.BankApp.API.DTOs.Bank;
using Technovert.BankApp.Models;


namespace Technovert.BankApp.API.Profiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<CreateDepositDTO, Transaction>();
            CreateMap<CreateTransferDTO, Transaction>();
        }
    }
}
