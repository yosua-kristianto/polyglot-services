using System.Text.Json.Serialization;

namespace AuthService.Messaging.Producer;

public class AuthenticationOTPMessageProducerDTO
{
    [JsonPropertyName("user_id")]
    public required Guid UserId { get; set; }

    [JsonPropertyName("recipient_name")]
    public required string RecipientName {get;set;}

    [JsonPropertyName("otp")]
    public required string Otp { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

}
