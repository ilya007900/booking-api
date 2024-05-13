namespace Booking.Domain.GeographicalData;

public class Country
{
    private readonly List<City> _cities = new();

    public int Id { get; }

    public string Name { get; }

    public IReadOnlyList<City> Cities => _cities.AsReadOnly();

    public Country(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddCity(City city)
    {
        _cities.Add(city);
    }
}