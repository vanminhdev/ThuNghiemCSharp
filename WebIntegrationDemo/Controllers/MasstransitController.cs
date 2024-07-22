using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebIntegrationDemo.Dtos.Messages;

namespace WebIntegrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasstransitController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MasstransitController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> PublishMessage([FromBody] MessageDto message)
        {
            await _publishEndpoint.Publish(message);
            return Ok();
        }
    }
}
