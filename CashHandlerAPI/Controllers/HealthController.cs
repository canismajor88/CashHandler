using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashHandlerAPI.Controllers
{
    public class HealthController : Controller
    {
        #region private

        private readonly ILogger<HealthController> _logger;

        #endregion

        #region constructors

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region endpoints

        [HttpGet]
        [Route("_health")]
        public ActionResult Get()
        {
            _logger.LogInformation("_health");
            return Ok();
        }

        #endregion
    }
}
