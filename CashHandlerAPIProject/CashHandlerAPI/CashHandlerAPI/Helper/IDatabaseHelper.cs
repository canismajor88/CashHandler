using System.Threading.Tasks;

namespace CashHandlerAPI.Helper
{
  public  interface IDatabaseHelper
  {
      public Task<bool> IsValidLogin(string userName, string password);
      public Task<bool> CreateNewUser(string userName, string password, string email);
      public Task<bool> ConfirmEmail(string userId, string token);
      public Task<bool> ChangePassword(string userId, string token, string newPassword);

    }
}
