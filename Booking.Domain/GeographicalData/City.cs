namespace Booking.Domain.GeographicalData;

public class City
{
    public int Id { get; }

    public string Name { get; }

    public Country Country { get; }

    public City(int id, string name, Country country)
    {
        Id = id;
        Name = name;
        Country = country;
    }
}