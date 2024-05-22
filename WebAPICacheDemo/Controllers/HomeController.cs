using Microsoft.AspNetCore.Mvc;

namespace WebAPICacheDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
