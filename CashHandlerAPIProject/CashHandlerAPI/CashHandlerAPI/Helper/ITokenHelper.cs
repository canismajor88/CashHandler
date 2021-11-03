using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.Helper
{
  public interface ITokenHelper
    {
        string GetUserName(string token);
        string GetToken(string token);
    }
}
