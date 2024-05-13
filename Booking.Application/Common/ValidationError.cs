namespace Booking.Application.Common;

public record ValidationError(string Message, string Property = "");