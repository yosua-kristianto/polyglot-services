using System;

namespace auth_service.Common.Exceptions;

public class UserNotActivatedException() : Exception(message: ErrorMessage)
{
    public const string Code = "UMA0006";
    private const string ErrorMessage = "The user account is not activated. Please activate your account before logging in.";
}
