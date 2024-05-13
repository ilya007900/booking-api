using Booking.Application.GeographicalData.Shared;
using Booking.Application.GeographicalData.Shared.Dtos;
using Booking.Domain.GeographicalData;
using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountryQuery;

public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDto>
{
    private readonly IGeographicalDataRepository _geographicalDataRepository;

    public GetCountryQueryHandler(IGeographicalDataRepository geographicalDataRepository)
    {
        _geographicalDataRepository = geographicalDataRepository;
    }

    public async Task<CountryDto> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await _geographicalDataRepository.GetCountry(request.CountryId, cancellationToken);
        return Mapper.From(country);
    }
}