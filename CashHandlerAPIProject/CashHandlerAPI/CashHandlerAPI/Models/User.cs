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

        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastSignIn { get; set; }
        public long? RegisterId { get; set; }

        public virtual Register? Register { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
