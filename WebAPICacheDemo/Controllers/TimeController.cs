using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace WebAPICacheDemo.Controllers
{
    [Route("api/time")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        /// <summary>
        /// Cache default (Cache-Control: public - default = any)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-time-default")]
        [ResponseCache(Duration = 30)]
        public IActionResult GetTime()
        {
            return Ok(DateTime.Now);
        }

        /// <summary>
        /// Cache ở client (Cache-Control: private - Location: client)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-time-client")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetTime2()
        {
            return Ok(DateTime.Now);
        }

        /// <summary>
        /// Cache ở client (Cache-Control: public - Location: Any)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-time-any")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult GetTime3()
        {
            return Ok(DateTime.Now);
        }

        /// <summary>
        /// Không cache (Cache-Control: public - Location: None)
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-time-none")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.None)]
        public IActionResult GetTime4()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet("get-time-by-filter")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetTimeByFilter(string keyword)
        {
            return Ok(DateTime.Now);
        }

        [HttpGet("get-image")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult GetImage()
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "10MB.jpg");
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            //var result = ResizeImage(fileStream);
            return File(fileStream, "image/jpeg");
        }

        private const int MAX_WIDTH = 1080;
        public static Stream ResizeImage(Stream source)
        {
            var image = Image.Load(source);
            int originalWidth = Math.Max(image.Width, image.Height);
            var jpegEncoder = new JpegEncoder { Quality = 70 }; // Điều chỉnh mức độ nén nếu là ảnh jpeg
            if (originalWidth <= MAX_WIDTH)
            {
                return source;
            }
            double resizeRatio = (double)MAX_WIDTH / originalWidth;
            image.Mutate(x => x.Resize((int)(image.Width * resizeRatio), (int)(image.Height * resizeRatio)));

            var memoryStream = new MemoryStream();
            image.Save(memoryStream, jpegEncoder);
            memoryStream.Position = 0;
            return memoryStream;
        }

        [HttpGet("get-image-2")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult GetImage2()
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "50KB.webp");
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "image/webp");
        }
    }
}
