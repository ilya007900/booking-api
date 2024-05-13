using MediatR;

namespace Booking.Application.GeographicalData.Queries.GetCountriesQuery;

public record GetCountriesQuery : IRequest<IReadOnlyList<GetCountriesQueryResult>>;