using Booking.Domain.GeographicalData;

namespace Booking.Infrastructure.Database.Repositories;

public class GeographicalDataRepository : IGeographicalDataRepository
{
    public Task<IReadOnlyList<Country>> GetCountries(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DataSource.Countries);
    }

    public Task<Country> GetCountry(int countryId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DataSource.Countries.Single(x => x.Id == countryId));
    }

    public Task<Country> GetCountryByCityId(int cityId, CancellationToken cancellationToken = default)
    {
        var country = DataSource.Countries.Single(x => x.Cities.Any(c => c.Id == cityId));
        return Task.FromResult(country);
    }
}