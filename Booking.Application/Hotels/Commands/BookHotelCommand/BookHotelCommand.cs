using MediatR;

namespace Booking.Application.Hotels.Commands.BookHotelCommand;

public record BookHotelCommand(int HotelId, DateTime Start, DateTime End) : IRequest;