using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public record ChangePasswordCredential
    {
        [Required]
        public string Token { get; init; }
        [Required]
        public string UserId { get; init; }
        [Required]
        public string NewPassword { get; init; }
    }
}
