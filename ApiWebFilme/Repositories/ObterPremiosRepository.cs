namespace ApiWebFilme.Repositories;

public class ObterPremiosRepository : IObterPremiosRepository
{
    public readonly FilmesContext _context;

    public ObterPremiosRepository(FilmesContext context)
        => _context = context;

    public async Task<ObterPremioViewModel> GetAsync()
    {

        var query = _context.Filmes
              .AsNoTracking()
              .Join(_context.Producers.AsNoTracking(),
                  filme => filme.Id,
                  producer => producer.FilmeId,
                  (filme, producer) => new { Filme = filme, Producer = producer })
              .Where(fp => fp.Filme.Winner == "yes")
              .GroupBy(fp => fp.Producer.Name)
              .Select(g => new ProducerViewModel
              {
                  PreviousWin = g.Min(fp => fp.Filme.Year),
                  FollowingWin = g.Max(fp => fp.Filme.Year),
                  Producer = g.Key,
                  Interval = g.Max(fp => fp.Filme.Year) - g.Min(fp => fp.Filme.Year)
              })
              .Where(result => result.Interval > 0)
              .OrderBy(result => result.Interval);

        var result = query.ToList();
        var minInterval = result.Min(r => r.Interval);
        var maxInterval = result.Max(r => r.Interval);

        return new ObterPremioViewModel
        {
            Min = result.Where(r => r.Interval == minInterval),
            Max = result.Where(r => r.Interval == maxInterval),
        };
    }
}