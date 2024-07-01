using MassTransit;
using System.Text.Json;
using WebIntegrationDemo.Dtos.Messages;

namespace WebIntegrationDemo.Consumers
{
    public class YourConsumer : IConsumer<MessageDto>
    {
        private readonly ILogger _logger;

        public YourConsumer(ILogger<YourConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MessageDto> context)
        {
            var message = context.Message;
            _logger.LogInformation(JsonSerializer.Serialize(message));
            return Task.CompletedTask;
        }
    }
}
