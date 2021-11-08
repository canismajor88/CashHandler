using System.ComponentModel.DataAnnotations;

namespace CashHandlerAPI.ViewModels
{
    public record ChangePasswordCredential
    {
        #region public propteies
        [Required]
        public string Token { get; init; }
        [Required]
        public string UserId { get; init; }
        [Required]
        public string NewPassword { get; init; }
        

        #endregion
      
     
    }
}
