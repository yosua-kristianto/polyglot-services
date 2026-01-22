using System;

namespace AuthService.Common.Exceptions;

public abstract class BaseCustomException : Exception
{
    public abstract string Code { get; }
    public abstract int StatusCode { get; }

    protected BaseCustomException(string message) : base(message) { }
}
