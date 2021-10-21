using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.Data
{
   public interface IUserCredentialsRepo
    {
        Task<bool> IsUser(UserCredential userCredentials);
    }
}
