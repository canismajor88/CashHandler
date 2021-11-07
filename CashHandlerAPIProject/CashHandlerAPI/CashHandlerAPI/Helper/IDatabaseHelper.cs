using CashHandlerAPI.ViewModels;
using System.Threading.Tasks;

namespace CashHandlerAPI.Helper
{
  public  interface IDatabaseHelper
  {
      public Task<bool> IsValidLogin(string userName, string password);
      public Task<bool> CreateNewUser(string userName, string password, string email);
      public Task<bool> ConfirmEmail(string userId, string token);
      public Task<bool> ChangePassword(string userId, string token, string newPassword);
      public Task<bool> InitializeMoneyAmount(MoneyAmountViewModel moneyAmountViewModel, string username);

      public Task<AddTransactionResult> RunTransaction(MoneyAmountViewModel moneyAmountViewModel, string username,
          decimal itemCost);

      public Task<MoneyAmountViewModel> GetMoneyAmountViewModel(string username);

  }
}
