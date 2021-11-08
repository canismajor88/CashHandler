using System.Collections.Generic;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.ViewModels
{
    public class GetTransactionsResult
    {
        #region public propeties

        public List<Transaction> Transactions { get; set; }
        public bool Success { get; set; }

        #endregion
        
    }
}
