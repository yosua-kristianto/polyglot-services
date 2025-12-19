using System;

namespace AuthService.Common.Exceptions;

public class DuplicatedUserException(): Exception(message: ErrorMessage)
{
    public const string Code = "UMA0005";
    private const string ErrorMessage = "Attempt to create a user that already exists. Please choose another email.";
}
