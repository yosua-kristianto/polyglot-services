using System;

namespace AuthService.Common.Exceptions;

public class UserNotActivatedException() : BaseCustomException(message: ErrorMessage)
{
    public override string Code => "UMA0006";

    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
    private const string ErrorMessage = "The user account is not activated. Please activate your account before logging in.";
}
