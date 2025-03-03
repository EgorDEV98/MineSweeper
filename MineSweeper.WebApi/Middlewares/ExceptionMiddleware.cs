using System.Net;
using System.Text.Json;

namespace MineSweeper.WebApi.Middlewares;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionMiddleware>();
}

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var msg = $"{exception.GetType()}\n {exception.Message}\n {exception.InnerException?.Message}\n {exception.StackTrace}";
            var response = new object[]
            {
                "error", exception.Message
            };
            _logger.LogError(msg);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}