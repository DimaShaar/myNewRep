using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeReposirtory _employeeReposirtory;

        public HomeController(ILogger<HomeController> logger, IEmployeeReposirtory employeeReposirtory)
        {
            _logger = logger;
            _employeeReposirtory = employeeReposirtory;
        }
        
        [Route("/")]
        [Route("Index")]       
        public IActionResult Index()
        {
           var model = _employeeReposirtory.GetAllEmployee();
            ViewBag.PageTitle = "Index";
           return View(model);
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}