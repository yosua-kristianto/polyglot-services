using System.Text.Json.Serialization;

namespace AuthService.Model.Object.Messaging.Producer;

public class AuthenticationOTPMessageProducerDTO
{
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    [JsonPropertyName("recipient_name")]
    public string RecipientName {get;set;}

    [JsonPropertyName("otp")]
    public string Otp { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

}
