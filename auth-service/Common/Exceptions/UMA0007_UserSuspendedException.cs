using System;

namespace AuthService.Common.Exceptions;

public class UserSuspendedException(): BaseCustomException(message: ErrorMessage)
{
    public override string Code => "UMA0007";
    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
    private const string ErrorMessage = "The user account has been suspended. Please contact support for more information.";
}
