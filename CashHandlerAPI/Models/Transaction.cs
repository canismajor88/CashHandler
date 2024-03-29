﻿using System;

#nullable disable

namespace CashHandlerAPI.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public double Amount { get; set; }
        public string Denominations { get; set; }
        public DateTime TransDate { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
