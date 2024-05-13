using Booking.Application.GeographicalData.Shared.Dtos;
using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountryByCityIdQuery;

public record GetCountryByCityIdQuery(int CityId) : IRequest<CountryDto>;