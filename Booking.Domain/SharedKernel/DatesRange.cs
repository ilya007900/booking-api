using CSharpFunctionalExtensions;

namespace Booking.Domain.SharedKernel;

public class DatesRange
{
    public DateTime Start { get; }

    public DateTime End { get; }

    public int Days => (End - Start).Days;

    private DatesRange(DateTime from, DateTime to)
    {
        Start = from.Date;
        End = to.Date;
    }

    public bool HasIntersect(DatesRange datesRange)
    {
        return Start <= datesRange.End && datesRange.Start <= End;
    }

    public static Result<DatesRange> TryCreate(DateTime start, DateTime end)
    {
        start = start.Date;
        end = end.Date;

        if (start > end)
        {
            return Result.Failure<DatesRange>(
                $"{nameof(start)} can't be later than {nameof(end)}. " +
                $"{nameof(start)}:{start:d}, {nameof(end)}:{end:d}");
        }

        return Result.Success(new DatesRange(start, end));
    }

    public static DatesRange Create(DateTime start, DateTime end)
    {
        var result = TryCreate(start, end);
        if (result.IsFailure)
        {
            throw new ArgumentException(result.Error);
        }

        return result.Value;
    }
}