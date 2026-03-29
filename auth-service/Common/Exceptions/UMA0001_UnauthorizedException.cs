using System;

namespace AuthService.Common.Exceptions;

public class UnauthorizedException() : BaseCustomException(message: ErrorMessage)
{
    private const string ErrorMessage = "Unauthorized.";

    public override string Code => "UMA0001";
    public override int StatusCode => StatusCodes.Status401Unauthorized;
}
