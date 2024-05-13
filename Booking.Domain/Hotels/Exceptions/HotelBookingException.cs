namespace Booking.Domain.Hotels.Exceptions;

public class HotelBookingException : Exception
{
    public HotelBookingException(string message) : base(message)
    {
        
    }
}