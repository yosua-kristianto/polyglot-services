using System;

namespace AuthService.Common.Exceptions;

public class UserSuspendedException(): BaseCustomException(message: ErrorMessage)
{
    public override string Code => "UMA0007";
    public override int StatusCode => StatusCodes.Status200OK;
    private const string ErrorMessage = "The user account has been suspended. Please contact support for more information.";
}
