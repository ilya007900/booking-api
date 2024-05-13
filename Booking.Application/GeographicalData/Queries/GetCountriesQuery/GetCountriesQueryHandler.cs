using Booking.Domain.GeographicalData;
using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountriesQuery;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IReadOnlyList<GetCountriesQueryResult>>
{
    private readonly IGeographicalDataRepository _geographicalDataRepository;

    public GetCountriesQueryHandler(IGeographicalDataRepository geographicalDataRepository)
    {
        _geographicalDataRepository = geographicalDataRepository;
    }

    public async Task<IReadOnlyList<GetCountriesQueryResult>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _geographicalDataRepository.GetCountries(cancellationToken);
        return countries.Select(From).ToArray();
    }

    private static GetCountriesQueryResult From(Country country)
    {
        return new GetCountriesQueryResult(country.Id, country.Name);
    }
}