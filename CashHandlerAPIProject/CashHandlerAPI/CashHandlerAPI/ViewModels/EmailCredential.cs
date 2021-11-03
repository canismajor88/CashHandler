using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public record EmailCredential
    {
        #region public propeties

        [Required]
        public string Token { get; init; }
        [Required]
        public string UserId { get; init; }

        #endregion
      
    }
}
