using Booking.Domain.SharedKernel;

namespace Booking.Domain.Test;

[TestFixture]
public class DatesRangeTests
{
    [Test]
    public void CanNotCreateIfFromBiggerThanTo()
    {
        var from = DateTime.Parse("01/10/2022");
        var to = DateTime.Parse("01/05/2022");

        Assert.Throws<ArgumentException>(() =>
        {
            _ = DatesRange.Create(from, to);
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
        var fromFifthToSeventhSeptember2020 = DatesRange.Create(
            DateTime.Parse("09/05/2020"), DateTime.Parse("09/15/2020"));

        var datesRange = DatesRange.Create(from, to);

        var hasCommonDates = datesRange.HasIntersect(fromFifthToSeventhSeptember2020);

        Assert.That(hasCommonDates, Is.True);
    }

    [Test]
    [TestCase("09/01/2020", "09/04/2020")]
    [TestCase("09/16/2020", "09/17/2020")]
    public void DoesNotHaveIntersect(DateTime from, DateTime to)
    {
        var fromFifthToSeventhSeptember2020 = DatesRange.Create(
            DateTime.Parse("09/05/2020"), DateTime.Parse("09/15/2020"));

        var datesRange = DatesRange.Create(from, to);

        var hasCommonDates = datesRange.HasIntersect(fromFifthToSeventhSeptember2020);

        Assert.That(hasCommonDates, Is.False);
    }

}