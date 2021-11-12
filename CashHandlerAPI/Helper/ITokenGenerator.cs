namespace CashHandlerAPI.Helper
{
  public  interface ITokenGenerator
    {
        string CreateToken(string userName);
    }
}
