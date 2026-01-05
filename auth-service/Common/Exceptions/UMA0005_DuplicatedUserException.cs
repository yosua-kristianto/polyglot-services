using System;

namespace AuthService.Common.Exceptions;

public class DuplicatedUserException(): BaseCustomException(message: ErrorMessage)
{
    private const string ErrorMessage = "Attempt to create a user that already exists. Please choose another email.";

    public override string Code => "UMA0005";
    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
}
