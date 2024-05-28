using MB.RabbitMQ.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;

namespace WebIntergrationDemo.Consumers
{
    public class RabbitMQDemo
    {
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbitMQDemo(IOptions<RabbitMqConfig> rabbitMqConfig)
        {
            _rabbitMqConfig = rabbitMqConfig.Value;
        }
    }
}
