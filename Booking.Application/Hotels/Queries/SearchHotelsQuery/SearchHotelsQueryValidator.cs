using Booking.Domain.SharedKernel;
using FluentValidation;

namespace Booking.Application.Hotels.Queries.SearchHotelsQuery;

public class SearchHotelsQueryValidator : AbstractValidator<SearchHotelsQuery>
{
    public SearchHotelsQueryValidator()
    {
        RuleFor(x => x.CityId).NotEmpty();
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