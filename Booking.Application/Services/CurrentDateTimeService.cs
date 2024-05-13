namespace Booking.Application.Services;

public class CurrentDateTimeService : ICurrentDateTimeService
{
    public DateTime Now => DateTime.Now;
}