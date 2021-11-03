using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.Models
{
    public class CreateUserCredential
    {
        [Required]
        public string UserName { get; init; }
        [Required]
        public string Password { get; init; }
        [Required]
        public string Email { get; init; }
    }
}
