
using MB.RabbitMQ.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using WebIntergrationDemo.Common;
using WebIntergrationDemo.Services.Consumers;
using WebIntergrationDemo.Services.Producers;

namespace WebIntergrationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.ConfigureRabbitMQ();
            builder.Services.AddSingleton<EditProducer>();
            builder.Services.AddSingleton<EditConsumer>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var rabbitMQConfig = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqConfig>>().Value;
                var connection = rabbitMQConfig.CreateConnection(true);
                var model = connection.CreateModel();

                var queueArgs = new Dictionary<string, object>
                {
                    { "x-queue-type", "quorum" }
                };
                model.QueueDeclare(QueueNames.EDIT + 1, durable: true, exclusive: false, autoDelete: false, arguments: queueArgs);
                model.ExchangeDeclare(ExchangeNames.EDIT, ExchangeType.Direct, durable: true, autoDelete: false);
                model.QueueBind(QueueNames.EDIT + 1, ExchangeNames.EDIT, "");
                model.BasicQos(prefetchSize: 0, prefetchCount: 20, global: true);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
