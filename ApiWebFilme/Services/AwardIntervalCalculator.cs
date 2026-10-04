namespace ApiWebFilme.Services;

public sealed record ProducerWin(string Producer, int Year);

public sealed class AwardIntervalCalculator
{
    public ObterPremioViewModel Calculate(IEnumerable<ProducerWin> wins)
    {
        ArgumentNullException.ThrowIfNull(wins);

        var intervals = wins
            .GroupBy(win => win.Producer, StringComparer.OrdinalIgnoreCase)
            .SelectMany(group =>
            {
                var years = group
                    .Select(win => win.Year)
                    .Distinct()
                    .OrderBy(year => year)
                    .ToArray();

                return years
                    .Zip(years.Skip(1), (previous, following) => new ProducerViewModel
                    {
                        Producer = group.Key,
                        PreviousWin = previous,
                        FollowingWin = following,
                        Interval = following - previous
                    });
            })
            .ToArray();

        if (intervals.Length == 0)
        {
            return new ObterPremioViewModel
            {
                Min = Array.Empty<ProducerViewModel>(),
                Max = Array.Empty<ProducerViewModel>()
            };
        }

        var minInterval = intervals.Min(result => result.Interval);
        var maxInterval = intervals.Max(result => result.Interval);

        return new ObterPremioViewModel
        {
            Min = intervals.Where(result => result.Interval == minInterval).ToArray(),
            Max = intervals.Where(result => result.Interval == maxInterval).ToArray()
        };
    }
}
