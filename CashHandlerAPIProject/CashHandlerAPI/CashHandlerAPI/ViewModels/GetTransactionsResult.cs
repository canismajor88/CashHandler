using System.Collections.Generic;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.ViewModels
{
    public class GetTransactionsResult
    {
        public List<Transaction> Transactions { get; set; }
        public bool Success { get; set; }
    }
}
