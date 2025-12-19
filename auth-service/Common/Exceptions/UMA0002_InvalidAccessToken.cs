using System;

namespace AuthService.Common.Exceptions;

public class InvalidAccessToken(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0002";
    private const string ErrorMessage = "Invalid access token.";
}
