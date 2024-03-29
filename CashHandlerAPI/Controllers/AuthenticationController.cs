﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
using CashHandlerAPI.Helper;
using CashHandlerAPI.Models;
using CashHandlerAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailHelper _emailHelper;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly UserManager<User> _userManager;
        private readonly CashHandlerDBContext _context;
        private readonly IDatabaseHelper _databaseHelper;

        #endregion

        #region constructors
        public AuthenticationController(ILogger<AuthenticationController> logger, ITokenGenerator tokenGenerator, IEmailHelper emailHelper, 
            IOptions<EmailOptions> emailOptions ,UserManager<User> userManager, CashHandlerDBContext context, IDatabaseHelper databaseHelper)
        {
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _emailHelper = emailHelper;
            _emailOptions = emailOptions;
            _userManager = userManager;
            _context = context;
            _databaseHelper = databaseHelper;
        }
        #endregion

        #region endPoints
        /// <summary>
        /// gets LoginCredential and returns result with a token
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginCredential userCredential)
        {
            try
            {
               
               
                if ( _databaseHelper.IsValidLogin(userCredential.UserName,userCredential.Password).Result)
                {
                   
                    _logger.Log(LogLevel.Information, "user was found");
                        var token = _tokenGenerator.CreateToken(userCredential.UserName);
                        //json sending back
                        Result result = new() { Payload = token, Status = typeof(OkResult), Success = true };
                        return Ok(new
                        {
                            Result = result
                        });
                }

                _logger.Log(LogLevel.Information, "user was not found");
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    succeeded = false
                });
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    succeeded = false
                });
            }
        }
        /// <summary>
        /// gets CreateUserCredential and returns result
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCredential userCredential)
        {
            try
            {
                

                if (_databaseHelper.CreateNewUser(userCredential.UserName,
                    userCredential.Password, userCredential.Email).Result)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userCredential.UserName);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmEmailUrl = Request.Headers["confirmEmailURL"];

                    var urlString=  _emailHelper.UrlStringBuilder(confirmEmailUrl, token, user.Id);

                    var emailBody = $"Please confirm your email by clicking on the link below </br>{urlString}";
                    const string? emailSubject = "Verification Email";
                    await _emailHelper.Send(userCredential.Email, emailBody, emailSubject,_emailOptions.Value);

                    var result = new Result
                    {
                        Payload = "user was added",
                        Status = Ok(),
                        Success = true
                    };
                        return Ok(new
                        {
                           result
                        });
                }
                var badResult = new Result
                {
                    Payload = "user was not added, there was a duplicate username or email",
                    Status =BadRequest(),
                    Success = true
                };
                return BadRequest(badResult);
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error,e.Message);
                var badResult = new Result
                {
                    Payload = "server error",
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                   badResult
                });
            }
        }
        /// <summary>
        /// gets EmailCredential and returns ok if email was confirmed
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailCredential userCredential)
        {
            try
            {
                var dbResult = await _databaseHelper.ConfirmEmail(userCredential.UserId, userCredential.Token);
                if (dbResult)
                {
                    return Ok();
                }

                return Unauthorized();


            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                var badResult = new Result
                {
                    Payload = "server error",
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    badResult
                });
            }
        }
        /// <summary>
        /// gets EmailCredential and returns ok if an email was sent to user to confirm email
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCredential userCredential)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userCredential.Email);
                if (user != null && user.EmailConfirmed)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordURL = Request.Headers["resetPasswordURL"];
                    var urlString = _emailHelper.UrlStringBuilder(resetPasswordURL, token, user.Id);
                    var emailBody = $"Please reset password by clicking on the link below </br>{urlString}";
                    const string? emailSubject = "Reset Password Email";
                    await _emailHelper.Send(userCredential.Email, emailBody, emailSubject, _emailOptions.Value);
                    return Ok();
                }

                return Unauthorized();


            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                var badResult = new Result
                {
                    Payload = "server error",
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    badResult
                });
            }
        }
        /// <summary>
        /// gets ChangePasswordCredential and returns ok if password was changed
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCredential userCredential)
        {
            try
            {
                var dbResult = await _databaseHelper.ChangePassword(userCredential.UserId, userCredential.Token,
                    userCredential.NewPassword);
                if (dbResult)
                {
                    return Ok();
                }

                return Unauthorized();


            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                var badResult = new Result
                {
                    Payload = "server error",
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false
                };
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    badResult
                });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("token-health")]
        public ActionResult Get([FromHeader] string authorization)
        {
            _logger.LogInformation("_health");
            return Ok();
        }

        #endregion


    }
}
