using AuthService.Api.Handler;
using AuthService.model.Object.Request;
using AuthService.model.Object.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Object;

namespace AuthService.Api.Controller;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthControllerHandler _authControllerHandler;

    public AuthController(IAuthControllerHandler authControllerHandler)
    {
        this._authControllerHandler = authControllerHandler;
    }

    /* Registration flow */

    [HttpPost("register")]
    public BaseResponseDTO<object> Register([FromBody] RegisterRequestDTO request)
    {
        _authControllerHandler.Register(request.Email, request.Name);

        return new BaseResponseDTO<object>
        {
            Data = null,
            Message = "Registration initiated. Please check your email for the OTP.",
            Status = true,
            Code = "200"
        };
    }

    [HttpPost("verify-email")]
    public BaseResponseDTO<LoginResponseDTO> VerifyEmailRegistration([FromBody] LoginRequestDTO request)
    {
        var response = _authControllerHandler.VerifyEmailRegistration(request.Email, request.Otp, request.DeviceId);

        return new BaseResponseDTO<LoginResponseDTO>
        {
            Data = response,
            Message = "Email verified successfully.",
            Status = true,
            Code = "200"
        };
    }

    /* Authentication flow */

    [HttpPost("invoke-otp")]
    public BaseResponseDTO<object> InvokeOTP([FromBody] AuthorizeOTPRequestDTO request)
    {
        _authControllerHandler.InvokeOTP(request.Email);

        return new BaseResponseDTO<object>
        {
            Data = null,
            Message = "OTP has been sent to your email.",
            Status = true,
            Code = "200"
        };
    }

    [HttpPost("login")]
    public BaseResponseDTO<LoginResponseDTO> Login([FromBody] LoginRequestDTO request)
    {
        var response = _authControllerHandler.Login(request.Email, request.Otp, request.DeviceId);
        return new BaseResponseDTO<LoginResponseDTO>
        {
            Data = response,
            Message = "Login successful",
            Status = true,
            Code = "200"
        };
    }


}
