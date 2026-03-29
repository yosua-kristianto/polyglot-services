namespace AuthService.Api.Auth.Dto.Request;

public class LoginRequestDTO
{
    public required string Email { get; set; }
    public required string Otp { get; set; }

    public string? DeviceId { get; set; }
}