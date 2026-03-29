using System;

namespace AuthService.Common.Exceptions;

public class InvalidAccessToken(): BaseCustomException(message: ErrorMessage)
{
    private const string ErrorMessage = "Invalid access token.";

    public override string Code => "UMA0002";
    public override int StatusCode => StatusCodes.Status401Unauthorized;
}
