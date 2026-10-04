using ApiWebFilme.Services;
using NUnit.Framework;

namespace TestApiWebFilmeApplication;

[TestFixture]
public class AwardIntervalCalculatorTests
{
    private readonly AwardIntervalCalculator _calculator = new();

    [Test]
    public void Calculate_UsesConsecutiveWins_NotOnlyFirstAndLast()
    {
        var wins = new[]
        {
            new ProducerWin("Producer A", 1990),
            new ProducerWin("Producer A", 1991),
            new ProducerWin("Producer A", 2000),
            new ProducerWin("Producer B", 1980),
            new ProducerWin("Producer B", 2000)
        };

        var result = _calculator.Calculate(wins);

        var min = result.Min.Single();
        var max = result.Max.Single();

        Assert.Multiple(() =>
        {
            Assert.That(min.Producer, Is.EqualTo("Producer A"));
            Assert.That(min.Interval, Is.EqualTo(1));
            Assert.That(min.PreviousWin, Is.EqualTo(1990));
            Assert.That(min.FollowingWin, Is.EqualTo(1991));

            Assert.That(max.Producer, Is.EqualTo("Producer B"));
            Assert.That(max.Interval, Is.EqualTo(20));
            Assert.That(max.PreviousWin, Is.EqualTo(1980));
            Assert.That(max.FollowingWin, Is.EqualTo(2000));
        });
    }

    [Test]
    public void Calculate_WhenNoProducerHasTwoWins_ReturnsEmptyCollections()
    {
        var wins = new[]
        {
            new ProducerWin("Producer A", 1990),
            new ProducerWin("Producer B", 2000)
        };

        var result = _calculator.Calculate(wins);

        Assert.Multiple(() =>
        {
            Assert.That(result.Min, Is.Empty);
            Assert.That(result.Max, Is.Empty);
        });
    }

    [Test]
    public void Calculate_IgnoresDuplicateYearsForSameProducer()
    {
        var wins = new[]
        {
            new ProducerWin("Producer A", 1990),
            new ProducerWin("Producer A", 1990),
            new ProducerWin("Producer A", 1995)
        };

        var result = _calculator.Calculate(wins);

        Assert.That(result.Min.Single().Interval, Is.EqualTo(5));
        Assert.That(result.Max.Single().Interval, Is.EqualTo(5));
    }
}
