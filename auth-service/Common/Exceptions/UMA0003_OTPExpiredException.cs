using System;

namespace AuthService.Common.Exceptions;

public class OTPExpiredException(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0003";
    private const string ErrorMessage = "OTP has expired.";
}