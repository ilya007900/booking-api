using Booking.Domain.GeographicalData;
using Booking.Domain.Hotels;
using Booking.Domain.Hotels.Exceptions;
using Booking.Domain.Photos;
using Booking.Domain.SharedKernel;

namespace Booking.Domain.Test;

[TestFixture]
public class HotelTests
{
    [Test]
    public void CanBookHotelIfNoOccupancy()
    {
        var hotel = CreateDefault();
        var datesRange = Helpers.CreateDatesRange("01/05/2021", "01/15/2021");
        var today = DateTime.Parse("01/04/2021");

        Assert.DoesNotThrow(() =>
        {
            hotel.Book(datesRange, today);
        });
    }

    [Test]
    public void CanBookHotelIfNotOccupied()
    {
        var hotel = CreateDefault(new []
        {
            Helpers.CreateDatesRange("01/16/2021", "01/17/2021")
        });

        var datesRange = Helpers.CreateDatesRange("01/05/2021", "01/15/2021");
        var today = DateTime.Parse("01/04/2021");

        Assert.DoesNotThrow(() =>
        {
            hotel.Book(datesRange, today);
        });
    }

    [Test]
    public void CanNotBookHotelIfOccupied()
    {
        var hotel = CreateDefault(new[]
        {
            Helpers.CreateDatesRange("01/08/2021", "01/17/2021")
        });

        var datesRange = Helpers.CreateDatesRange("01/05/2021", "01/15/2021");
        var today = DateTime.Parse("01/04/2021");

        Assert.Throws<HotelBookingException>(() =>
        {
            hotel.Book(datesRange, today);
        });
    }

    [Test]
    public void CanNotBookIfDaysLessThanOne()
    {
        var hotel = CreateDefault();
        var datesRange = Helpers.CreateDatesRange("01/05/2021", "01/05/2021");
        var today = DateTime.Parse("01/04/2021");

        Assert.Throws<HotelBookingException>(() =>
        {
            hotel.Book(datesRange, today);
        });
    }

    [Test]
    public void CanNotBookInPast()
    {
        var hotel = CreateDefault();
        var datesRange = Helpers.CreateDatesRange("01/05/2021", "01/06/2021");
        var today = DateTime.Parse("01/07/2021");

        Assert.Throws<HotelBookingException>(() =>
        {
            hotel.Book(datesRange, today);
        });
    }

    private static Hotel CreateDefault(
        IEnumerable<DatesRange>? occupancy= null,
        DateTime? today = null)
    {
        var hotel = new Hotel(
            1,
            "hotel1",
            new City(1, "City1", new Country(1, "Country1")),
            5f,
            200,
            new List<Photo>());

        if (occupancy != null)
        {
            foreach (var datesRange in occupancy)
            {
                hotel.Book(datesRange, today ?? DateTime.MinValue);
            }
        }

        return hotel;
    }
}