using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Mime;
using CashHandlerAPI.ViewModels;
using Microsoft.Extensions.Logging;
using CashHandlerAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using CashHandlerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CashHandlerAPI.Controllers
{
    public class TransactionController : Controller
    {
        #region private

        private readonly ILogger<TransactionController> _logger;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly ITokenHelper _tokenHelper;

        #endregion

        #region constructors

         public TransactionController(ILogger<TransactionController> logger, IDatabaseHelper databaseHelper, 
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
        [Route("run-transaction")]
        public async Task<IActionResult> RunTransaction([FromBody] MoneyAmountViewModel moneyAmount,
            [FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = await _databaseHelper.RunTransaction(moneyAmount, username, (decimal)moneyAmount.TransactionAmount);

                if (dbResult.Success)
                {
                    return Ok(dbResult);
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
        [Route("get-transactions")]
        public async Task<IActionResult> GetTransactions([FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = await _databaseHelper.GetTransactions(username);

                if (dbResult.Success)
                {
                    IList<TransactionViewModel> transactionViewModels = dbResult.Transactions.Select(transaction => new TransactionViewModel { TransactionId = transaction.TransactionId, TransDate = transaction.TransDate, Amount = transaction.Amount, Denominations = transaction.Denominations }).ToList();

                    return Ok(transactionViewModels);
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
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("get-transaction")]
        public async Task<IActionResult> GetTransaction([FromBody] GetTransactionViewModel getTransactionViewModel)
        {
            try
            {

                Transaction dbResult = await _databaseHelper.GetTransaction(getTransactionViewModel.TransactionId);

                if (dbResult != null)
                {
                    return Ok(new TransactionViewModel
                    {
                        Amount = dbResult.Amount,
                        Denominations = dbResult.Denominations,
                        TransactionId = dbResult.TransactionId,
                        TransDate = dbResult.TransDate
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
