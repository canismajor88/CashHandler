using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using CashHandlerAPI.Data;
using CashHandlerAPI.Helper;
using CashHandlerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        private readonly IEmailHelper _emailHelper;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly UserManager<User> _userManager;

        #endregion

        #region constructors
        public AuthenticationController(ILogger<AuthenticationController> logger, IUserCredentialsRepo userRepo, ITokenGenerator tokenGenerator
            , IEmailHelper emailHelper, IOptions<EmailOptions> emailOptions ,UserManager<User> userManager)
        {
            _logger = logger;
            _iUserCredentialsRepo = userRepo;
            _tokenGenerator = tokenGenerator;
            _emailHelper = emailHelper;
            _emailOptions = emailOptions;
            _userManager = userManager;
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
                    Result result = new() { Payload = token, Status = typeof(OkResult), Success = true };
                    return Ok(new
                    {
                       Result=result
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
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCredential userCredential)
        {
            try
            {
                var isFound = await _iUserCredentialsRepo.IsUser(userCredential);
                var newUser = new User
                {
                    UserName = userCredential.UserName,
                    Email = userCredential.Email
                };
                var result = await _userManager.CreateAsync(newUser, userCredential.Password);
                if (!isFound&&result.Succeeded)
                { _logger.Log(LogLevel.Information,"user was added");
                    if(await _iUserCredentialsRepo.AddUser(userCredential))
                        //json sending back

                        return Ok(new
                        {
                            succeeded = true
                        });
                }
                _logger.Log(LogLevel.Information, "user was not added");
                return BadRequest(result);
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
