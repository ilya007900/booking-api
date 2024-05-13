namespace Booking.Application.Hotels.Queries.SearchHotelsQuery;

public record SearchHotelsQueryResult(
    int Id,
    string Name,
    string City,
    string Country,
    float Rating,
    decimal PricePerNight,
    decimal TotalPrice,
    int TotalNights,
    string? PhotoUrl,
    bool Occupied);