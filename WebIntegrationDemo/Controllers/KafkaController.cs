using Microsoft.AspNetCore.Mvc;
using WebIntegrationDemo.Consumers;

namespace WebIntegrationDemo.Controllers
{
    [Route("api/kafka")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        [HttpGet("get-message")]
        public void GetMessage()
        {
            KafkaDemo.Test();
        }
    }
}
