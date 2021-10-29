using System;
using System.Collections.Generic;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class Register
    {
        public Register()
        {
            Users = new HashSet<User>();
        }

        public long RegisterId { get; set; }
        public string RegisterLocation { get; set; }
        public long? CashAmountId { get; set; }

        public virtual ICollection<User>? Users { get; set; }
    }
}
