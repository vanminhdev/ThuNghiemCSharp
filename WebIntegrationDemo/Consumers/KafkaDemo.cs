using Confluent.Kafka;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace WebIntegrationDemo.Consumers
{
    public class KafkaDemo
    {
        public static IConsumer<Ignore, string> BuildConsumer()
        {
            var config = new ConsumerConfig
            {
                GroupId = "my-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                
            };

            return new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public static void Test()
        {
            using var consumer = BuildConsumer();
            consumer.Subscribe("Topic2");
            try
            {
                var cr = consumer.Consume(millisecondsTimeout: 200);
                if (cr != null) 
                {
                    Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occured: {e.Error.Reason}");
            }
            consumer.Close();
        }
    }
}
