namespace AuthService.Api.Auth.Dto.Request;

public class RegisterRequestDTO
{
    public required string Email { get; set; }
    public required string Name { get; set; }
}