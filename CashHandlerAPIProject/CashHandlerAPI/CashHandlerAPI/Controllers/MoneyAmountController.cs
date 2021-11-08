using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using CashHandlerAPI.Helper;
using CashHandlerAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CashHandlerAPI.Controllers
{
    public class MoneyAmountController : Controller
    {
      

        #region private

        private readonly ILogger<TransactionController> _logger;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly ITokenHelper _tokenHelper;

        #endregion

        #region constructors

        public MoneyAmountController(ILogger<TransactionController> logger, IDatabaseHelper databaseHelper,
            ITokenHelper tokenHelper)
        {
            _logger = logger;
            _databaseHelper = databaseHelper;
            _tokenHelper = tokenHelper;
        }


        #endregion

        #region endpoints

        [Authorize]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("update-moneyAmount")]
        public async Task<IActionResult> UpdateMoneyAmount([FromBody] MoneyAmountViewModel moneyAmount,
            [FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = await _databaseHelper.UpdateMoneyAmount(moneyAmount, username);

                if (dbResult)
                {
                    return Ok(new Result
                    {
                        Payload = _databaseHelper.GetMoneyAmountViewModel(username).Result
                    });
                }

                return BadRequest();


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
        [Route("get-moneyAmount")]
        public async Task<IActionResult> GetMoneyAmount([FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = _databaseHelper.GetMoneyAmountViewModel(username);
                if (dbResult.Result.Success)
                {
                    return Ok(new Result
                    {
                        Payload = dbResult.Result
                    });
                }

                return BadRequest();


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

        #endregion

    }
}
