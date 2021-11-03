using System.Threading.Tasks;

namespace CashHandlerAPI.Helper
{
  public  interface IDatabaseHelper
  {
      public Task<bool> IsValidUserNameAndPassword(string userName, string password);
      public Task<bool> CreateNewUser(string userName, string password, string email);
  }
}
