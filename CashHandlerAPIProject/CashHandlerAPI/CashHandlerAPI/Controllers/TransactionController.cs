using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CashHandlerAPI.Models;
using System.Net.Mime;
using CashHandlerAPI.ViewModels;
using Microsoft.Extensions.Logging;
using CashHandlerAPI.Helper;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System;
using CashHandlerAPI.Data;
using Microsoft.AspNetCore.Http;

namespace CashHandlerAPI.Controllers
{
    public class TransactionController : Controller
    {

        private readonly ILogger<TransactionController> _logger;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailHelper _emailHelper;
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly UserManager<User> _userManager;
        private readonly CashHandlerDBContext _context;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly ITokenHelper _tokenHelper;

        public TransactionController(ILogger<TransactionController> logger, ITokenGenerator tokenGenerator,
            UserManager<User> userManager, CashHandlerDBContext context, IDatabaseHelper databaseHelper, 
            ITokenHelper tokenHelper)
        {
            _logger = logger;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _context = context;
            _databaseHelper = databaseHelper;
            _tokenHelper = tokenHelper;
        }

        // POST: TransactionController/Create
        [HttpPost]
        [Route("Add")]
        public async Task<StatusCodeResult> Create()
        {

            return null;
        }

        //// GET: TransactionController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: TransactionController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: TransactionController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TransactionController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("initialize-moneyamount")]
        public async Task<IActionResult> InitializeMoneyAmount([FromBody] MoneyAmountViewModel moneyAmount,
            [FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = await _databaseHelper.InitializeMoneyAmount(moneyAmount, username);
                
                if (dbResult)
                {
                    return Ok(new Result
                    {
                        Payload = _databaseHelper.GetMoneyAmountViewModel(username).Result
                    });
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

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("run-transaction")]
        public async Task<IActionResult> RunTransaction([FromBody] MoneyAmountViewModel moneyAmount,
            [FromHeader] string authorization)
        {
            try
            {
                var username = _tokenHelper.GetUserName(_tokenHelper.GetToken(authorization));
                var dbResult = await _databaseHelper.RunTransaction(moneyAmount, username,(decimal)moneyAmount.TransactionAmount);

                if (dbResult)
                {
                    return Ok( new Result
                    {
                        Payload = _databaseHelper.GetMoneyAmountViewModel(username).Result
                    });
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
    }

}
