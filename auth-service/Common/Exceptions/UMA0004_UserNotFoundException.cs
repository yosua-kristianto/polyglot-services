using System;

namespace AuthService.Common.Exceptions;

public class UserNotFoundException(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0004";
    private const string ErrorMessage = "User not found.";
}
