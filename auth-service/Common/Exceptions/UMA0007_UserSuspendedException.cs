using System;

namespace auth_service.Common.Exceptions;

public class UserSuspendedException(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0007";
    private const string ErrorMessage = "The user account has been suspended. Please contact support for more information.";
}
