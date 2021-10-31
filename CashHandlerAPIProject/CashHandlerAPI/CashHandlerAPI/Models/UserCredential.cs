﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.Models
{
    //record like class but not mutable
    public record UserCredential
    {
        #region public properties
        //init ( initialize instead of set because records are immutable)records have build in comparison operators so we don't have to make our own
        [Required]
        public string UserName { get; init; }
        [Required]
        public string Password { get; init; }
        #endregion
    }
}
