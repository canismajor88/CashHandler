using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
using CashHandlerAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CashHandlerAPI.Repos
{
    public class UserCredentialRepo:IUserCredentialsRepo
    {
        #region private fields
        private readonly HashSet<UserCredential> _myUsers = new()
        {
            new UserCredential
            {
                Password = "strongPassword",
                UserName = "CoolGuy6969"
            }
        };
        #endregion
        #region public methods
        public Task<bool> IsUser(UserCredential userCredentials)
        {
            // this is where we would run a stored procedure to find if user is in DB
            return Task.FromResult(_myUsers.Contains(userCredentials));
        }
        #endregion
    }
}
