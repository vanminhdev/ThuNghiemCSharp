using Microsoft.AspNetCore.Mvc;

namespace ServerSendEvent.Controllers
{
    [Route("api/sse")]
    [ApiController]
    public class SseController : ControllerBase
    {
        private readonly ILogger<SseController> _logger;

        public SseController(ILogger<SseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("events")]
        public async Task GetEvents()
        {
            var id = Guid.NewGuid();

            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Connection", "keep-alive");
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(5000); // Simulate event creation
                var newEvent = $"Event at {DateTime.Now}";
                _logger.LogInformation($"Sending event to {id}, message = {newEvent}");
                await Response.WriteAsync($"data: {newEvent}\n\n");
                await Response.Body.FlushAsync();
            }
        }

        public class MessageDto
        {
            public string? Message { get; set; }
        }

        [HttpPost("log")]
        public IActionResult Log([FromBody] MessageDto messageDto)
        {
            _logger.LogInformation(messageDto.Message);
            return Ok();
        }

    }
}
