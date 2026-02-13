using System.Net;
using System.Text.Json;
using BankingSystem.SharedKernel.Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.WebApi.Configurations.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, context.TraceIdentifier);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
    {
        var (status, title, detail) = exception switch
        {
            DomainException dex => (HttpStatusCode.UnprocessableEntity, "Business Rule Violation", dex.Message),
            KeyNotFoundException _ => (HttpStatusCode.NotFound, "Resource Not Found", exception.Message),
            UnauthorizedAccessException _ => (HttpStatusCode.Unauthorized, "Unauthorized Access", exception.Message),
            DbUpdateConcurrencyException _ => (HttpStatusCode.Conflict, "Concurrency Conflict", "The data was updated by another process. Please try again."),
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error", exception.Message)
        };

        if ((int)status >= 500)
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        var problem = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = (int)status,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = traceId;

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)status;
        
        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, jsonOptions));
    }
}