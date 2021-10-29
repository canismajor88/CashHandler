using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashHandlerAPI.Models;

namespace CashHandlerAPI.Controllers
{
    public class TransactionController : Controller
    {

        private readonly CashHandlerDBContext _context;

        public TransactionController(CashHandlerDBContext context)
        {
            _context = context;
        }


        // POST: TransactionController/Create
        [HttpPost]
        [Route("Add")]
        public async Task<StatusCodeResult> Create()
        {
            try
            {
                await _context.CashBalances.AddAsync(new CashBalance
                {
                    Amount = 69,
                    Currency = "Blaze"
                });
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
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
    }
}
