using Microsoft.AspNetCore.Mvc;
using WebIntergrationDemo.Consumers;

namespace WebIntergrationDemo.Controllers
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
