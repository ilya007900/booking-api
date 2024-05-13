using Booking.Application.GeographicalData.Shared.Dtos;
using Booking.Domain.GeographicalData;

namespace Booking.Application.GeographicalData.Shared;

public static class Mapper
{
    public static CityDto From(City city)
    {
        return new CityDto(city.Id, city.Name);
    }

    public static CountryDto From(Country country)
    {
        return new CountryDto(country.Id, country.Name, country.Cities.Select(From).ToArray());
    }
}