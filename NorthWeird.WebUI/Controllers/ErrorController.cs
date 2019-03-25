using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            _logger.LogError(error, error.Message);
            return View(error);
        }
    }
}
