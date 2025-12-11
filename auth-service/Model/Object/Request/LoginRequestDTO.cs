namespace AuthService.model.Object.Request;

public class LoginRequestDTO
{
    public string Email { get; set; }
    public string Otp { get; set; }

    public string DeviceId { get; set; }
}