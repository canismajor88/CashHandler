using System;
using System.Collections.Generic;

#nullable disable

namespace CashHandlerAPI.Models
{
    public partial class MoneyAmount
    {
        public MoneyAmount()
        {
            AspNetUsers = new HashSet<User>();
        }

        public long MoneyAmountId { get; set; }
        public int? DollarCoinAmount { get; set; }
        public int? HalfDollarAmount { get; set; }
        public int? QuartersAmount { get; set; }
        public int? DimesAmount { get; set; }
        public int? NicklesAmount { get; set; }
        public int? PenniesAmount { get; set; }
        public int? HundredsAmount { get; set; }
        public int? FiftiesAmount { get; set; }
        public int? TwentiesAmount { get; set; }
        public int? TensAmount { get; set; }
        public int? FivesAmount { get; set; }
        public int? OnesAmount { get; set; }
        public int? TotalAmount { get; set; }

        public virtual ICollection<User> AspNetUsers { get; set; }
    }
}
