using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public class TransactionViewModel
    {
        public long  TransactionId { get; set; }

        public double Amount { get; set; }

        public string Denominations { get; set; }

        public DateTime TransDate { get; set; }
    }
}
