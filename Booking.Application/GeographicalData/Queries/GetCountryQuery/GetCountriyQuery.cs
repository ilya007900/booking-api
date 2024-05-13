using Booking.Application.GeographicalData.Shared.Dtos;
using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountryQuery;

public record GetCountryQuery(int CountryId) : IRequest<CountryDto>;