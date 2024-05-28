using MB.RabbitMQ.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MB.RabbitMQ
{
    public abstract class RabbitMqService
    {
        private readonly RabbitMqConfig _config;

        public RabbitMqService(IOptions<RabbitMqConfig> options)
        {
            _config = options.Value;
        }

        public virtual IConnection CreateConnection()
        {
            return _config.CreateConnection(true);
        }
    }
}
