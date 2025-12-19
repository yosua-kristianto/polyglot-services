using System;
using Confluent.Kafka;

namespace AuthService.Config.Kafka;

public sealed class KafkaContext : IDisposable
{
    public ProducerConfig ProducerConfig { get; }

    public KafkaContext (IConfiguration configuration)
    {
        this.ProducerConfig = new ProducerConfig
        {
            BootstrapServers =
                configuration.GetValue<string>("Kafka:BootstrapServers") ?? throw new InvalidOperationException("Kafka:BootstrapServers not set")
        };
    }

    public IProducer<string, string> CreateProducer()
    {
        return new ProducerBuilder<string, string>(this.ProducerConfig).Build();
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
