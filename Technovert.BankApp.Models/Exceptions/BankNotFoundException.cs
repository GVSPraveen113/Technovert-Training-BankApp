﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    [Serializable]
    public class BankNotFoundException:Exception
    {
        public BankNotFoundException()
        {
        }
        public BankNotFoundException(string message) : base(message) { }
        
    }
}
