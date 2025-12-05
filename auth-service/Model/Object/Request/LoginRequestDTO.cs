namespace AuthService.model.Object.Request;

class LoginRequestDTO
{
    public string Email { get; set; }
    public string Otp { get; set; }
}