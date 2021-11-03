using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CashHandlerAPI.Data;
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
    }
}
