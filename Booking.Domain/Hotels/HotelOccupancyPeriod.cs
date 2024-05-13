using Booking.Domain.SharedKernel;

namespace Booking.Domain.Hotels;

public class HotelOccupancyPeriod
{
    public DatesRange DatesRange { get; }

    public HotelOccupancyPeriod(DatesRange datesRange)
    {
        DatesRange = datesRange;
    }
}