using System.ComponentModel.DataAnnotations;

namespace CashHandlerAPI.ViewModels
{
    public class CreateUserCredential
    {
        #region public properties
        [Required]
         public string UserName { get; init; }
        [Required]
        public string Password { get; init; }
        [Required]
        public string Email { get; init; }

        #endregion
        
       
    }
}
