using System;
using System.Collections.Generic;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class CashBalance
    {
        public long CashBalanceId { get; set; }
        public double? Amount { get; set; }
        public string Currency { get; set; }
    }
}
