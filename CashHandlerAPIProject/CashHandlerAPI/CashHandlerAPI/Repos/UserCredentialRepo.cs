using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
using CashHandlerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CashHandlerAPI.Repos
{
    public class UserCredentialRepo:IUserCredentialsRepo
    {
        #region private fields
        private HashSet<UserCredential> _myUsers = new()
        {
            new UserCredential
            {
                Password = "Admin",
                UserName = "Admin"
            }
        };
        #endregion
        #region public methods
        public Task<bool> IsUser(UserCredential userCredentials)
        {
            // this is where we would run a stored procedure to find if user is in DB
            return Task.FromResult(_myUsers.Contains(userCredentials));
        }

        public Task<bool> AddUser(UserCredential userCredential)
        {
            try
            {
                _myUsers.Add(userCredential);
                return Task.FromResult(true);
            }catch
            {
                return Task.FromResult(false);
            }
        }
        #endregion
    }
}
