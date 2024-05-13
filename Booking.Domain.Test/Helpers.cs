using Booking.Domain.SharedKernel;

namespace Booking.Domain.Test;

public static class Helpers
{
    public static DatesRange CreateDatesRange(string from, string to)
    {
        return DatesRange.Create(DateTime.Parse(from), DateTime.Parse(to));
    }
}