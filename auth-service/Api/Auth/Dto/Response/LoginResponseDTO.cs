namespace AuthService.Api.Auth.Dto.Response;

public class LoginResponseDTO
{
    public required string AccessToken {get; set;}
    public string? RefreshToken {get; set;}
}