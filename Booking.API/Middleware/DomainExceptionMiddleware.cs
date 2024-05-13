using Booking.Domain.Hotels.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Middleware;

public class DomainExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public DomainExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HotelBookingException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "HotelBookingFailure",
                Title = "Hotel Booking error",
                Detail = "Can't book hotel",
                Extensions =
                {
                    ["errors"] = exception.Message
                }
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}