using System;

namespace AuthService.Common.Exceptions;

public class UnauthorizedException() : Exception(message: ErrorMessage)
{
    public const string Code = "UMA0001";
    private const string ErrorMessage = "Unauthorized.";
}
