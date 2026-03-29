using System;

namespace AuthService.Common.Exceptions;

public class OTPExpiredException(): BaseCustomException(message: ErrorMessage)
{
    private const string ErrorMessage = "OTP has expired.";

    public override string Code => "UMA0003";
    public override int StatusCode => StatusCodes.Status419AuthenticationTimeout;
}