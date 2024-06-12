using MB.RabbitMQ.Configs;
using MB.RabbitMQ.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MB.RabbitMQ
{
    public class ProducerService : RabbitMqService, IProducerService
    {
        protected IModel _model = null!;
        protected IConnection _connection = null!;
        public ProducerService(IOptions<RabbitMqConfig> options) : base(options)
        {
        }

        public virtual void PublishMessage<TEntity>(TEntity? entity, string exchangeName, string bindingKey) where TEntity : class
        {
        }
    }
}
