using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Enums
{
    public enum BankStaffOptions
    {
        CreateAccount=1,
        UpdateAccount,
        DeleteAccount,
        AddNewCurrency,
        AddServiceChargeSameBank,
        AddServiceChargeDiffBank,
        ViewAccountTransactionHistory,
        RevertTransaction,
        Exit
    }
}
