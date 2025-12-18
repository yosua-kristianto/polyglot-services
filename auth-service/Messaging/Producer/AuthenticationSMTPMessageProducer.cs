using System;
using System.Text.Json;
using auth_service.Config.Kafka;
using AuthService.Model.Object.Messaging.Producer;
using Confluent.Kafka;

namespace auth_service.Messaging.Producer;

public sealed class AuthenticationSMTPMessageProducer
{
    public const string TOPIC = "nma-service-01-authotp";

    private readonly KafkaContext _ctx;

    public AuthenticationSMTPMessageProducer(KafkaContext ctx)
    {
        this._ctx = ctx;
    }

    public async Task Send(AuthenticationOTPMessageProducerDTO dto)
    {
        using var producer = this._ctx.CreateProducer();

        var payload = JsonSerializer.Serialize(dto);

        Console.WriteLine($"Payload: {payload}");

        await producer.ProduceAsync(
            TOPIC,
            new Confluent.Kafka.Message<string, string>
            {
                Key = dto.UserId.ToString(),
                Value = payload
            }
        );

        producer.Flush(TimeSpan.FromSeconds(5));
    }
}
