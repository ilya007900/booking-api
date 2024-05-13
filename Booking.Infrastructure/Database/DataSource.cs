using Bogus;
using Booking.Domain.GeographicalData;
using Booking.Domain.Hotels;
using Booking.Domain.Photos;

namespace Booking.Infrastructure.Database;

public static class DataSource
{
    public static readonly IReadOnlyList<Country> Countries;
    public static readonly IReadOnlyList<Hotel> Hotels;

    static DataSource()
    {
        Randomizer.Seed = new Random(8615301);

        Countries = GenerateCountries(10);
        Hotels = GenerateHotels(1000);
    }

    private static IReadOnlyList<Country> GenerateCountries(int number)
    {
        var countryIds = 1;
        var cityIds = 1;
        var countriesFaker = new Faker<Country>().CustomInstantiator(f =>
        {
            var country = new Country(countryIds++, f.Address.Country());
            var citiesCount = f.Random.Int(1, 5);
            for (var i = 0; i < citiesCount; i++)
            {
                country.AddCity(new City(cityIds++, f.Address.City(), country));
            }

            return country;
        });

        return countriesFaker.Generate(number).OrderBy(x => x.Name).ToArray();
    }

    private static IReadOnlyList<Hotel> GenerateHotels(int number)
    {
        var hotelIds = 1;
        var hotelsFaker = new Faker<Hotel>().CustomInstantiator(f => new Hotel(
            hotelIds++,
            f.Company.CompanyName(),
            f.PickRandom((IEnumerable<City>)f.PickRandom((IEnumerable<Country>)Countries).Cities),
            f.Random.Float(3, 5),
            f.Finance.Amount(20, 2000, 0),
            new List<Photo>
            {
                new Photo(f.Image.PicsumUrl(320, 480))
            }));

        return hotelsFaker.Generate(number);
    }
}