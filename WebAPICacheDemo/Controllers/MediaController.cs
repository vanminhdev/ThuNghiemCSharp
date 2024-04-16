using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICacheDemo.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        [HttpGet("video")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetVideo()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "video-demo.mp4");
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //var bytes = System.IO.File.ReadAllBytes(path);
            return File(fileStream, "video/mp4");
        }
    }
}
