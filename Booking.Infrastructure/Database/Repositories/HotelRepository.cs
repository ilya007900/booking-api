using Booking.Domain.Hotels;

namespace Booking.Infrastructure.Database.Repositories;

public class HotelRepository : IHotelRepository
{
    public Task<Hotel> GetById(int hotelId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DataSource.Hotels.Single(x => x.Id == hotelId));
    }

    public Task<IReadOnlyList<Hotel>> SearchBy(int cityId, CancellationToken cancellationToken = default)
    {
        var hotels = DataSource.Hotels
            .Where(x => x.City.Id == cityId)
            .ToArray();

        return Task.FromResult(hotels as IReadOnlyList<Hotel>);
    }
}