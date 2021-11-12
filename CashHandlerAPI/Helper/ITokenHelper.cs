namespace CashHandlerAPI.Helper
{
  public interface ITokenHelper
    {
        string GetUserName(string token);
        string GetToken(string token);
    }
}
