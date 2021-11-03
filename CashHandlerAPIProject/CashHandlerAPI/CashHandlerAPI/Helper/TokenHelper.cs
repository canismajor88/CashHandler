﻿using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CashHandlerAPI.Helper
{
    public class TokenHelper:ITokenHelper
    {
        #region public methods
        public string GetUserName(string token)
        {

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
            var userName = jsonToken?.Claims.FirstOrDefault(claims => claims.Type == "unique_name")?.Value;
            return userName;
        }

        public string GetToken(string authorizationString)
        {
            //takes Bearer off of authorization so we can just get the token string
            var tokenString = authorizationString.Split(' ').Last();
            return tokenString;
        }

        #endregion
       
    }
}
