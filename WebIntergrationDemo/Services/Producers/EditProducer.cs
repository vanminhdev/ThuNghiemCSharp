using MB.RabbitMQ;
using MB.RabbitMQ.Configs;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Threading.Channels;

namespace WebIntergrationDemo.Services.Producers
{
    public class EditProducer : ProducerService
    {
        public EditProducer(IOptions<RabbitMqConfig> options) : base(options)
        {
            _connection = CreateConnection();
            _model = _connection.CreateModel();
        }

        public override void PublishMessage<TEntity>(TEntity? entity, string exchangeName, string bindingKey) where TEntity : class
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(entity);
            _model.BasicPublish(exchangeName, bindingKey, true, null, body);
        }
    }
}
