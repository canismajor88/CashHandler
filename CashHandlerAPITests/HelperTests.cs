using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashHandlerAPI.Helper;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace CashHandlerAPITests
{
    class HelperTests
    {
        private Mock<IConfiguration> _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c["TokenSettings:ExpirationLength"]).Returns("1");
            _configuration.Setup(c => c["TokenSettings:Key"]).Returns("SecretKey420Blaze!");
            _configuration.Setup(c => c["TokenSettings:IssuerLocation"]).Returns("https://localhost:5001");
            _configuration.Setup(c => c["TokenSettings:AudienceLocation"]).Returns("https://localhost:5001");
        }
        [Test]
       public void TokenGeneratorTest()
        {
            //sut = system under test
            var sut = new TokenGenerator(_configuration.Object);
            var token = sut.CreateToken("BobBuilder");
            var tokenHelper = new TokenHelper();
            Assert.True("BobBuilder" == tokenHelper.GetUserName(token));
            var now = DateTime.Now;
            var nowHours = now.Hour;
            var exp = tokenHelper.GetExpirationDate(token);
            var hoursToExpire = exp.Hour - nowHours;
            Assert.True(hoursToExpire == 1);
        }

       [Test]
      public void TokenGeneratorExpiredTokenTest()
       {
           
           const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkJvYiIsIm5iZiI6MTYzODkyNzkwMCwiZXhwIjoxNjM4OTMxNTAwLCJpYXQiOjE2Mzg5Mjc5MDAsImlzcyI6Imh0dHBzOi8vY2FzaGhhbmRsZXJhcHAuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSJ9.673qg7_umIBIffAxlfsMdDysqUJahF5zNRVgF-Jhko4";
           //sut
           var sut = new TokenHelper();
           Assert.True("Bob" == sut.GetUserName(token));
           var now = DateTime.Now;
           var exp = sut.GetExpirationDate(token);
           var hoursToExpire = exp - now;
           Assert.True(hoursToExpire.ToString().Contains("-"));
       }

      [Test]
     public void EmailHelperUrlStringBuilder()
      {
          var token =
              "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkJvYiIsIm5iZiI6MTYzODkyNzkwMCwiZXhwIjoxNjM4OTMxNTAwLCJpYXQiOjE2Mzg5Mjc5MDAsImlzcyI6Imh0dHBzOi8vY2FzaGhhbmRsZXJhcHAuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSJ9.673qg7_umIBIffAxlfsMdDysqUJahF5zNRVgF-Jhko4";
          var receiverAddress = "https://localhost:5001";
          var userId = "1234";
          var sut = new EmailHelper();
          Assert.True("https://localhost:5001/?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkJvYiIsIm5iZiI6MTYzODkyNzkwMCwiZXhwIjoxNjM4OTMxNTAwLCJpYXQiOjE2Mzg5Mjc5MDAsImlzcyI6Imh0dHBzOi8vY2FzaGhhbmRsZXJhcHAuYXp1cmV3ZWJzaXRlcy5uZXQvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSJ9.673qg7_umIBIffAxlfsMdDysqUJahF5zNRVgF-Jhko4&userid=1234"
                      == sut.UrlStringBuilder(receiverAddress, token, userId));

      }
    }
}
