using ApiWebFilme.Services;

namespace ApiWebFilme.Repositories;

public class ObterPremiosRepository : IObterPremiosRepository
{
    private readonly FilmesContext _context;
    private readonly AwardIntervalCalculator _calculator;

    public ObterPremiosRepository(
        FilmesContext context,
        AwardIntervalCalculator calculator)
    {
        _context = context;
        _calculator = calculator;
    }

    public async Task<ObterPremioViewModel> GetAsync()
    {
        var winningEntries = await _context.Filmes
            .AsNoTracking()
            .Join(
                _context.Producers.AsNoTracking(),
                filme => filme.Id,
                producer => producer.FilmeId,
                (filme, producer) => new
                {
                    Producer = producer.Name,
                    filme.Year,
                    filme.Winner
                })
            .Where(entry => entry.Winner == "yes")
            .Select(entry => new
            {
                entry.Producer,
                entry.Year
            })
            .ToListAsync();

        var wins = winningEntries
            .Select(entry => new ProducerWin(entry.Producer, entry.Year));

        return _calculator.Calculate(wins);
    }
}
