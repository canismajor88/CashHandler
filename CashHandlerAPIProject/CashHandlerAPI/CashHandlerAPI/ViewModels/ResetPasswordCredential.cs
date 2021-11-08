using System.ComponentModel.DataAnnotations;

namespace CashHandlerAPI.ViewModels
{
    public record ResetPasswordCredential
    {
        

        #region public propeties

         [Required]
         public string Email { get; init; }

        #endregion
       
    }
}
