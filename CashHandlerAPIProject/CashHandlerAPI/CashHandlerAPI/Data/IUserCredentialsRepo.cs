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
        public Task<bool> AddUser(UserCredential userCredential);
        Task<bool> IsUser(CreateUserCredential userCredentials);
        Task<bool> AddUser(CreateUserCredential userCredential);
    }
}
