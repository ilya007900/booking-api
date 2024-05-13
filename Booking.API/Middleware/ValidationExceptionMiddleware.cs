using Booking.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Middleware;

public sealed class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred",
                Extensions =
                {
                    ["errors"] = exception.Errors
                }
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}