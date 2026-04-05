using AuthService.Common;
using AuthService.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace AuthService.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IOptions<JsonOptions> jsonOptions
    )
    {
        _next = next;
        _logger = logger;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        BaseResponseDTO<object> response;
        int statusCode;

        if (exception is BaseCustomException appEx)
        {
            statusCode = appEx.StatusCode;
            response = BaseResponseDTO<object>.Error(appEx.Message, appEx.Code);
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            response = BaseResponseDTO<object>.Error(
                "Internal Server Error: " + exception.Message,
                "500"
            );
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(
                response, 
                _jsonOptions
            )
        );
    }
}
