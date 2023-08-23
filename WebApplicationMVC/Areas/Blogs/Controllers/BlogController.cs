using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMVC.Areas.Blogs.Controllers
{
    [Area("Blogs")]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Blogs/Views/Blog/Index.cshtml");
        }
    }
}
