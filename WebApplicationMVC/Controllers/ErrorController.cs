using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logeer)
        {
            _logger = logeer;
        }

        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
            var StatusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry,the resource your requested could not be found";
                    _logger.LogWarning($"Error 404 Occured Path ={StatusCodeResult?.OriginalPath}"+
                        $" Query String = {StatusCodeResult?.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error() {
        
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The Path{exceptionDetails?.Path} throw an exception"+
                $"{exceptionDetails?.Error.Message}");         
            return View();
        }
    }
}
