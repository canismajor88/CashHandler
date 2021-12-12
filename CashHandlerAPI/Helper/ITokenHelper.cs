using System;

namespace CashHandlerAPI.Helper
{
  public interface ITokenHelper
    {
        string GetUserName(string token);
        string GetToken(string token);
        public DateTime GetExpirationDate(string token);
    }
}
