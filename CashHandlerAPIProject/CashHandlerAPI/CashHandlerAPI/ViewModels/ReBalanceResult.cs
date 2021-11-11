using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public class ReBalanceResult
    {
        #region publicProperties

        public bool Success { get; set; }
        public string TakeOutString { get; set; }

        #endregion
    }
}
