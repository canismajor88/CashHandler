using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public class AddTransactionResult
    {
        public string? GiveBackString { get; set; }
        public MoneyAmountViewModel? MoneyAmountViewModel { get; set; }
        public bool Success  { get; set; }
    }
}
