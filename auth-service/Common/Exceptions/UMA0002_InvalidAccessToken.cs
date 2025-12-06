using System;

namespace auth_service.Common.Exceptions;

public class InvalidAccessToken(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0002";
    private const string ErrorMessage = "Invalid access token.";
}
