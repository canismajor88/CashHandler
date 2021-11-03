using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.ViewModels
{
    public record ResetPasswordCredential
    {
        [Required]
        public string Email { get; init; }
    }
}
