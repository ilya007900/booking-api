using Booking.Application.Services;
using Booking.Domain.Hotels;
using Booking.Domain.SharedKernel;
using MediatR;

namespace Booking.Application.Hotels.Queries.SearchHotelsQuery;

public class SearchHotelsQueryHandler :
    IRequestHandler<SearchHotelsQuery, IReadOnlyList<SearchHotelsQueryResult>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ICurrentDateTimeService _currentDateTimeService;

    public SearchHotelsQueryHandler(
        IHotelRepository hotelRepository,
        ICurrentDateTimeService currentDateTimeService)
    {
        _hotelRepository = hotelRepository;
        _currentDateTimeService = currentDateTimeService;
    }

    public async Task<IReadOnlyList<SearchHotelsQueryResult>> Handle(
        SearchHotelsQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.SearchBy(request.CityId, cancellationToken);

        var datesRange = DatesRange.Create(request.Start, request.End);
        var today = _currentDateTimeService.Now.Date;
        return hotels.Select(x => From(x, datesRange, today)).ToArray();
    }

    private static SearchHotelsQueryResult From(
        Hotel hotel, 
        DatesRange dates, 
        DateTime today)
    {
        return new SearchHotelsQueryResult(
            hotel.Id,
            hotel.Name,
            hotel.City.Name,
            hotel.City.Country.Name,
            hotel.Rating,
            hotel.PricePerNight,
            hotel.CalculatePriceForDatesRange(dates),
            dates.Days,
            hotel.Photos.FirstOrDefault()?.Url,
            !hotel.CanBook(dates, today));
    }
}