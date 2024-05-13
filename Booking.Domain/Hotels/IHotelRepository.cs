namespace Booking.Domain.Hotels;

public interface IHotelRepository
{
    Task<Hotel> GetById(int hotelId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Hotel>> SearchBy(int cityId, CancellationToken cancellationToken = default);
}