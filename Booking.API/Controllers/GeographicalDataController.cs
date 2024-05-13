using Booking.Application.GeographicalData.Queries.GetCountriesQuery;
using Booking.Application.GeographicalData.Queries.GetCountryByCityIdQuery;
using Booking.Application.GeographicalData.Queries.GetCountryQuery;
using Booking.Application.GeographicalData.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class GeographicalDataController : BaseController
    {
        public GeographicalDataController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet("countries")]
        public Task<IReadOnlyList<GetCountriesQueryResult>> Get(CancellationToken cancellationToken)
        {
            return Mediator.Send(new GetCountriesQuery(), cancellationToken);
        }

        [HttpGet("countries/{countryId}")]
        public Task<CountryDto> GetCountry([FromRoute] int countryId, CancellationToken cancellationToken)
        {
            return Mediator.Send(new GetCountryQuery(countryId), cancellationToken);
        }

        [HttpGet("countries/search")]
        public Task<CountryDto> GetCountryByCityId([FromQuery] int cityId, CancellationToken cancellationToken)
        {
            return Mediator.Send(new GetCountryByCityIdQuery(cityId), cancellationToken);
        }
    }
}
