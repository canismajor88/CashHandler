using System.ComponentModel.DataAnnotations;

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
