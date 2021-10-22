using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
using CashHandlerAPI.Helper;
using CashHandlerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CashHandlerAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region private
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserCredentialsRepo _iUserCredentialsRepo;
        private readonly ITokenGenerator _tokenGenerator;
        #endregion

        #region constructors
        public AuthenticationController(ILogger<AuthenticationController> logger, IUserCredentialsRepo userRepo, ITokenGenerator tokenGenerator)
        {
            _logger = logger;
            _iUserCredentialsRepo = userRepo;
            _tokenGenerator = tokenGenerator;
        }
        #endregion

        #region endPoints

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserCredential userCredential)
        {
            try
            {
                var isFound = await _iUserCredentialsRepo.IsUser(userCredential);
                if (isFound)
                {
                    _logger.Log(LogLevel.Information, "user was found");
                    var token = _tokenGenerator.CreateToken(userCredential.UserName);
                    //json sending back
                    return Ok(new
                    {
                        token = token
                        ,
                        succeeded = true
                    });
                }
                _logger.Log(LogLevel.Information, "user was not found");
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    succeeded = false
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    succeeded = false
                });
            }
        }

        #endregion


    }
}
