using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace CashHandlerAPI.Models
{
    public class User:IdentityUser
    {
        #region contructors

        public User()
        {
            Transactions = new HashSet<Transaction>();
        }

        #endregion

        #region public properties

        public DateTime? LastSignIn { get; set; }
        public MoneyAmount? MoneyAmount { get; set; }
        public long? MoneyAmountId { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        #endregion
     
    }
}
