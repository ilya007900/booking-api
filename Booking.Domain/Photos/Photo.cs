namespace Booking.Domain.Photos;

public class Photo
{
    public string Url { get; }

    public Photo(string url)
    {
        Url = url;
    }
}