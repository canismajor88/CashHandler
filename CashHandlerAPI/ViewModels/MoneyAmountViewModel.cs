﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CashHandlerAPI.ViewModels
{
    public record MoneyAmountViewModel
    {
        #region public propeties

        [Required]
        public int DollarCoinAmount { get; set; }
        [Required]
        public int HalfDollarAmount { get; set; }
        [Required]
        public int QuartersAmount { get; set; }
        [Required]
        public int DimesAmount { get; set; }
        [Required]
        public int NicklesAmount { get; set; }
        [Required]
        public int PenniesAmount { get; set; }
        [Required]
        public int HundredsAmount { get; set; }
        [Required]
        public int FiftiesAmount { get; set; }
        [Required]
        public int TwentiesAmount { get; set; }
        [Required]
        public int TensAmount { get; set; }
        [Required]
        public int FivesAmount { get; set; }
        [Required]
        public int OnesAmount { get; set; }
        [AllowNull]
        public decimal? TotalAmount { get; set; }
        [AllowNull]
        public decimal? TransactionAmount { get; set; }

        [AllowNull]
        public string? Description { get; set; }
        #endregion

    }
}
