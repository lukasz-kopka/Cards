using System.Net.Mime;
using System.Net;
using System.Text.Json;

using Cards.Core.Exceptions;
using ILogger = Serilog.ILogger;

namespace Cards.API.Middleware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ExceptionHandlingMiddleware(ILogger logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = (int)GetStatusCodeFrom(ex);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionJson(ex.Message, statusCode), _jsonOptions));

        var data = new { requestPath = context.Request.Path.Value, exceptionType = ex.GetType().Name, message = ex.Message };
        if (statusCode == (int)HttpStatusCode.NotFound)
        {
            _logger.Warning(LoggerTemplates.ExceptionOccured, data);
        }
        else
        {
            _logger.Error(ex, LoggerTemplates.ExceptionOccured, data);
        }
    }

    private HttpStatusCode GetStatusCodeFrom(Exception ex)
    {
        switch (ex)
        {
            case InvalidActionException:
                return HttpStatusCode.BadRequest;

            case NotFoundException:
                return HttpStatusCode.NotFound;

            default:
                return HttpStatusCode.InternalServerError;
        }
    }

    public record ExceptionJson(string ExceptionMessage, int StatusCode);
}
