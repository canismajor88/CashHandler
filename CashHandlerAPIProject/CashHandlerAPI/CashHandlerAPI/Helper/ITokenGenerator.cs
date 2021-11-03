using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashHandlerAPI.Helper
{
  public  interface ITokenGenerator
    {
        string CreateToken(string userName);
    }
}
