using MB.RabbitMQ.Configs;
using MB.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using WebIntegrationDemo.Common;
using WebIntegrationDemo.Dtos.Messages;
using System.Threading;

namespace WebIntegrationDemo.Services.Consumers
{
    public class EditConsumer : ConsumerService
    {
        private readonly ILogger<EditConsumer> _logger;

        public EditConsumer(IOptions<RabbitMqConfig> config, ILogger<EditConsumer> logger) : base(config, QueueNames.EDIT + 1)
        {
            _connection = CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueBind(_queueName, ExchangeNames.EDIT, "");
            _logger = logger;
        }

        protected override async Task ReceiveMessage(object sender, BasicDeliverEventArgs basic, Guid consumerId)
        {
            var body = basic.Body.ToArray();
            var obj = JsonSerializer.Deserialize<MessageDto>(body);
            int threadId = Thread.CurrentThread.ManagedThreadId;

            _logger.LogInformation($"[threadId = {threadId}, consumerId = {consumerId}] ReceiveMessage: {JsonSerializer.Serialize(obj)}");
            if (obj!.Message == "1")
            {
                Task.Delay(1000).Wait();
            }

            await Task.CompletedTask;
            _model.BasicAck(basic.DeliveryTag, false); 
        }
    }
}
