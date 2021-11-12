using CashHandlerAPI.ViewModels;
using System.Threading.Tasks;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.Helper
{
  public  interface IDatabaseHelper
  {
        #region Auth

      public Task<bool> IsValidLogin(string userName, string password);
      public Task<bool> CreateNewUser(string userName, string password, string email);
      public Task<bool> ConfirmEmail(string userId, string token);
      public Task<bool> ChangePassword(string userId, string token, string newPassword);

      #endregion

        #region Transactions

        public Task<AddTransactionResult> RunTransaction(MoneyAmountViewModel moneyAmountViewModel, string username,
          decimal itemCost);

        public Task<GetTransactionsResult> GetTransactions(string username);

        public Task<Transaction> GetTransaction(long transactionId);

        #endregion

        #region MoneyAmounts

        public Task<GetMoneyAmountResult> GetMoneyAmountViewModel(string username);
        public Task<bool> UpdateMoneyAmount(MoneyAmountViewModel moneyAmountViewModel, string username);
        public Task<ReBalanceResult> ReBalanceMoneyAmount(decimal targetAmount, string username);
        #endregion



    }
}
