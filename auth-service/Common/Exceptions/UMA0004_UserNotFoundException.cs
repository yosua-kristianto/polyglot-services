using System;

namespace AuthService.Common.Exceptions;

public class UserNotFoundException(): BaseCustomException(message: ErrorMessage)
{
    private const string ErrorMessage = "User not found.";

    public override string Code => "UMA0004";
    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
}
