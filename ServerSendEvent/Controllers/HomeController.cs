using Microsoft.AspNetCore.Mvc;

namespace ServerSendEvent.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
