using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class User:IdentityUser
    {
        public User()
        {
            Transactions = new HashSet<Transaction>();
        }

        public DateTime? LastSignIn { get; set; }
        public long? CashBalanceId { get; set; }
        public MoneyAmount? MoneyAmount { get; set; }
        public long? MoneyAmountId { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public CashBalance CashBalance { get; set; }
    }
}
