using MB.RabbitMQ.Configs;
using MB.RabbitMQ.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MB.RabbitMQ
{
    public abstract class ConsumerService : RabbitMqService, IConsumerService
    {
        protected IModel _model = null!;
        protected IConnection _connection = null!;
        protected string _queueName = null!;

        public ConsumerService(IOptions<RabbitMqConfig> config, string queueName) : base(config)
        {
            _queueName = queueName;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }

        public async Task ReadMessages(bool exclusive = false)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            Guid consumerId = Guid.NewGuid();
            consumer.Received += async (sender, basic) =>
            {
                await ReceiveMessage(sender, basic, consumerId);
            };
            _model.BasicConsume(consumer, _queueName, autoAck: false, exclusive: exclusive);
            await Task.CompletedTask;
        }

        protected abstract Task ReceiveMessage(object sender, BasicDeliverEventArgs basic, Guid consumerId);
    }
}
