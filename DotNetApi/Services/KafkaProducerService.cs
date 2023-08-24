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

    public async Task ProduceAsync(string topic, string eventName, object data)
    {
        var message = new Message<string, string>
        {
            Key = eventName,
            Value = JsonConvert.SerializeObject(data)
        };

        await _producer.ProduceAsync(topic, message);
    }
}
