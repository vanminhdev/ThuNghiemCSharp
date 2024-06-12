using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest2.Controllers
{
    [Route("value")]
    public class ValuesController : Controller
    {
        [HttpGet("redirect")]
        public IActionResult RedirectUrl(string returnUrl)
        {
            return Redirect(returnUrl);
        }
    }
}
