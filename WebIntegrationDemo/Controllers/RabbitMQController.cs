using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebIntegrationDemo.Common;
using WebIntegrationDemo.Consumers;
using WebIntegrationDemo.Dtos.Messages;
using WebIntegrationDemo.Services.Consumers;
using WebIntegrationDemo.Services.Producers;

namespace WebIntegrationDemo.Controllers
{
    [Route("api/rabbit")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private readonly EditProducer _editProducer;

        public RabbitMQController(EditProducer editProducer)
        {
            _editProducer = editProducer;
        }

        [HttpPost("push-message")]
        public void PushMessage(string message)
        {
            _editProducer.PublishMessage(new MessageDto { Message = message }, ExchangeNames.EDIT, "");
        }

        [HttpPost("create-consumer")]
        public async Task CreateConsumer()
        {
            var _editConsumer = HttpContext.RequestServices.GetRequiredService<EditConsumer>();
            await _editConsumer.ReadMessages(false);
        }
    }
}
