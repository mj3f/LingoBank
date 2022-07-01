using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace LingoBank.API.Middleware;

/// <summary>
/// Catch-all exceptions that occur from API requests, and log the error(s).
/// </summary>
public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger = Log.ForContext<ExceptionMiddleware>();

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error("Unhandled Exception has been thrown, details are below:");
            _logger.Error($"Request: {context.Request.Method}, Path: {context.Request.Path.Value}, Status Code: {context.Response.StatusCode}");
            _logger.Error($"Exception: {ex.Message}");
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy =
                    JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(new {Status = context.Response.StatusCode, Message = "Internal server error"}, options);

            await context.Response.WriteAsync(json);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}