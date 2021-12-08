using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CashHandlerAPI.Helper
{
    public class TokenHelper:ITokenHelper
    {
        #region public methods
        /// <summary>
        /// gets user name out of a token
        /// </summary>
        public string GetUserName(string token)
        {

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
            var userName = jsonToken?.Claims.FirstOrDefault(claims => claims.Type == "unique_name")?.Value;
            return userName;
        }
        /// <summary>
        /// gets a token from an authorizationString
        /// </summary>
        public string GetToken(string authorizationString)
        {
            //takes Bearer off of authorization so we can just get the token string
            var tokenString = authorizationString.Split(' ').Last();
            return tokenString;
        }

        public DateTime GetExpirationDate(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;
            var exp = jsonToken?.Claims.FirstOrDefault(claims => claims.Type == "exp")?.Value;
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).DateTime.ToUniversalTime().AddHours(-12);
        }

        #endregion
       
    }
}
