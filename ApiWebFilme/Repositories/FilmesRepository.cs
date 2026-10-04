namespace ApiWebFilme.Repositories;

public class FilmesRepository : IFilmesRepository
{
    private readonly FilmesContext _context;

    public FilmesRepository(FilmesContext context)
        => _context = context;

    public async Task CreateAsync(List<Filme> filmes)
    {
        await _context.Filmes.AddRangeAsync(filmes);
        await _context.SaveChangesAsync();
    }
}
