using Booking.Domain.SharedKernel;
using FluentValidation;

namespace Booking.Application.Hotels.Commands.BookHotelCommand;

public class BookHotelCommandValidator : AbstractValidator<BookHotelCommand>
{
    public BookHotelCommandValidator()
    {
        RuleFor(x => x.HotelId).NotEmpty();
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End).NotEmpty();
        RuleFor(x => x).Custom((x, context) =>
        {
            var result = DatesRange.TryCreate(x.Start, x.End);
            if (result.IsFailure)
            {
                context.AddFailure(result.Error);
            }
        });
    }
}