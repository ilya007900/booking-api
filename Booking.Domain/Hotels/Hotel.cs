using Booking.Domain.GeographicalData;
using Booking.Domain.Hotels.Exceptions;
using Booking.Domain.Photos;
using Booking.Domain.SharedKernel;

namespace Booking.Domain.Hotels;

public class Hotel
{
    private static object _lock = new object();

    private readonly List<HotelOccupancyPeriod> _occupancy = new();

    public int Id { get; }

    public string Name { get; }

    public City City { get; }

    public float Rating { get; }

    public decimal PricePerNight { get; }

    public IReadOnlyList<Photo> Photos { get; }

    public IReadOnlyList<HotelOccupancyPeriod> Occupancy => _occupancy.AsReadOnly();

    public Hotel(int id, string name, City city, float rating, decimal pricePerNight, IReadOnlyList<Photo> photos)
    {
        Id = id;
        Name = name;
        City = city;
        Rating = rating;
        PricePerNight = pricePerNight;
        Photos = photos;
    }

    public void Book(DatesRange datesRange, DateTime today)
    {
        lock (_lock)
        {
            if (!CanBook(datesRange, today))
            {
                throw new HotelBookingException("Can't book hotel.");
            }

            var occupancy = new HotelOccupancyPeriod(datesRange);
            _occupancy.Add(occupancy);
        }
    }

    public bool CanBook(DatesRange datesRange, DateTime today)
    {
        if (today.Date >= datesRange.Start)
        {
            return false;
        }

        if (datesRange.Days < 1)
        {
            return false;
        }

        return !Occupancy.Any(x => x.DatesRange.HasIntersect(datesRange));
    }

    public decimal CalculatePriceForDatesRange(DatesRange datesRange)
    {
        return PricePerNight * datesRange.Days;
    }
}