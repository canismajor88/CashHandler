using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CashHandlerAPI.Helper
{
    public class TokenGenerator : ITokenGenerator
    {

        #region private
        private readonly IConfiguration _configuration;
        #endregion

        #region constructor
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region publicMethods
        public string CreateToken(string userName)
        {
            var timeGap = short.Parse(_configuration["TokenSettings:ExpirationLength"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("TokenSettings:Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name,userName)
                }),
                Issuer = _configuration["TokenSettings:IssuerLocation"],
                Audience = _configuration["TokenSettings:AudienceLocation"],
                Expires = DateTime.UtcNow.AddHours(timeGap),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
