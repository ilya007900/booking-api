namespace Booking.Domain.GeographicalData;

public interface IGeographicalDataRepository
{
    Task<IReadOnlyList<Country>> GetCountries(CancellationToken cancellationToken = default);

    Task<Country> GetCountry(int countryId, CancellationToken cancellationToken = default);

    Task<Country> GetCountryByCityId(int cityId, CancellationToken cancellationToken = default);
}