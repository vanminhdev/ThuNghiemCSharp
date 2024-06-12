using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static System.Net.WebRequestMethods;

namespace WebIntergrationDemo.Controllers
{
    [Route("api/proxy/google")]
    [ApiController]
    public class GoogleApiController : ControllerBase
    {
        private const string ApiKey = "YOUR_API_KEY"; //cấu hình lấy từ appsettings

        public GoogleApiController()
        {
        }

        /// <summary>
        /// Phần anyurl là đường dẫn sau domain https://maps.googleapis.com <br/>
        /// ví dụ: https://maps.googleapis.com/maps/api/place/autocomplete/json thì anyurl là: /maps/api/place/autocomplete/json và url thực tế khi call sẽ là
        /// api/proxy/google/maps/maps/api/place/autocomplete/json <br/>
        /// sau đó truyền các url parameter khác nằm sau đó ngoại trừ &key=YOUR_API_KEY sẽ được thêm vào tự động
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [HttpPut]
        [HttpDelete]
        [HttpPatch]
        [HttpHead]
        [Route("maps/{*anyurl}")]
        public async Task<IActionResult> ForwardRequest()
        {
            var client = new HttpClient();

            string path = Request.Path.Value?.Replace("/api/proxy/google/maps", string.Empty) ?? string.Empty;

            // Build the request URL
            var requestUrl = $"https://maps.googleapis.com/{path}{Request.QueryString}&key={ApiKey}";

            // Create the request message
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(Request.Method),
                RequestUri = new Uri(requestUrl)
            };

            // Copy headers
            //foreach (var header in Request.Headers)
            //{
            //    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            //}

            // Copy content if present
            if (Request.ContentLength > 0)
            {
                requestMessage.Content = new StreamContent(Request.Body);
                foreach (var header in Request.Headers)
                {
                    requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            // Send the request
            var response = await client.SendAsync(requestMessage);

            // Copy the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            // Return the response to the client
            return StatusCode((int)response.StatusCode, responseContent);
        }
    }
}
