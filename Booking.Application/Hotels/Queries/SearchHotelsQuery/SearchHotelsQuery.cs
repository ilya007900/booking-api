using MediatR;

namespace Booking.Application.Hotels.Queries.SearchHotelsQuery;

public record SearchHotelsQuery(
    int CityId, DateTime Start, DateTime End) : IRequest<IReadOnlyList<SearchHotelsQueryResult>>;