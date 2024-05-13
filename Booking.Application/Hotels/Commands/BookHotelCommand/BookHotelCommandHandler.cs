using Booking.Application.Services;
using Booking.Domain.Hotels;
using Booking.Domain.SharedKernel;
using MediatR;

namespace Booking.Application.Hotels.Commands.BookHotelCommand;

public class BookHotelCommandHandler : IRequestHandler<BookHotelCommand>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ICurrentDateTimeService _currentDateTimeService;

    public BookHotelCommandHandler(
        IHotelRepository hotelRepository,
        ICurrentDateTimeService currentDateTimeService)
    {
        _hotelRepository = hotelRepository;
        _currentDateTimeService = currentDateTimeService;
    }

    public async Task Handle(BookHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetById(request.HotelId, cancellationToken);

        var bookingDates = DatesRange.Create(request.Start, request.End);
        var today = _currentDateTimeService.Now.Date;

        hotel.Book(bookingDates, today);
    }
}