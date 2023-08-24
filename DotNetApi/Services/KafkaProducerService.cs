using Confluent.Kafka;
using Newtonsoft.Json;

namespace DotNetApi.Services;
public class KafkaProducerService
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService()
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "kafka:9092",
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    public async Task ProduceAsync(string topic, string eventName, string id, string name, string distance)
    {
        var message = new Message<string, string>
        {
            Key = eventName,
            Value = JsonConvert.SerializeObject(new
            {
                Id = id,
                Name = name,
                Distance = distance
            })
        };

        await _producer.ProduceAsync(topic, message);
    }
}
