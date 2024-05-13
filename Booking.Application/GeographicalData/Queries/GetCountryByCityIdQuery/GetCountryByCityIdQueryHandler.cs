using Booking.Application.GeographicalData.Shared;
using Booking.Application.GeographicalData.Shared.Dtos;
using Booking.Domain.GeographicalData;
using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountryByCityIdQuery;

public class GetCountryByCityIdQueryHandler : IRequestHandler<GetCountryByCityIdQuery, CountryDto>
{
    private readonly IGeographicalDataRepository _geographicalDataRepository;

    public GetCountryByCityIdQueryHandler(IGeographicalDataRepository geographicalDataRepository)
    {
        _geographicalDataRepository = geographicalDataRepository;
    }

    public async Task<CountryDto> Handle(GetCountryByCityIdQuery request, CancellationToken cancellationToken)
    {
        var country = await _geographicalDataRepository.GetCountryByCityId(request.CityId, cancellationToken);

        return Mapper.From(country);
    }
}