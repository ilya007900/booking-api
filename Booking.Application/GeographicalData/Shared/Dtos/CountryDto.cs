namespace Booking.Application.GeographicalData.Shared.Dtos;

public record CountryDto(int Id, string Name, IReadOnlyList<CityDto> Cities);