using Booking.Domain.SharedKernel;

namespace Booking.Domain.Test;

[TestFixture]
public class DatesRangeTests
{
    [Test]
    public void CanNotCreateIfStartLaterThanEnd()
    {
        var start = DateTime.Parse("01/10/2022");
        var end = DateTime.Parse("01/05/2022");

        Assert.Throws<ArgumentException>(() =>
        {
            _ = DatesRange.Create(start, end);
        });
    }

    [Test]
    [TestCase("09/05/2020", "09/15/2020")]
    [TestCase("09/05/2020", "09/16/2020")]
    [TestCase("09/04/2020", "09/15/2020")]
    [TestCase("09/03/2020", "09/16/2020")]
    [TestCase("09/01/2020", "09/05/2020")]
    [TestCase("09/07/2020", "09/09/2020")]
    [TestCase("09/01/2020", "09/30/2020")]
    public void HasIntersect(DateTime from, DateTime to)
    {
        var datesRange1 = Helpers.CreateDatesRange("09/05/2020", "09/15/2020");
        var datesRange = DatesRange.Create(from, to);

        var hasCommonDates = datesRange.HasIntersect(datesRange1);

        Assert.That(hasCommonDates, Is.True);
    }

    [Test]
    [TestCase("09/01/2020", "09/04/2020")]
    [TestCase("09/16/2020", "09/17/2020")]
    public void DoesNotHaveIntersect(DateTime from, DateTime to)
    {
        var datesRange1 = Helpers.CreateDatesRange("09/05/2020", "09/15/2020");
        var datesRange = DatesRange.Create(from, to);

        var hasCommonDates = datesRange.HasIntersect(datesRange1);

        Assert.That(hasCommonDates, Is.False);
    }

}