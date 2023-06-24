namespace ApiWebFilme.Repositories;

public class FilmesRepository : IFilmesRepository
{
    public readonly FilmesContext _context;

    public FilmesRepository(FilmesContext context)
        => _context = context;

    public async Task CreateAsync(List<Filme> filme)
    {
        await _context.Filmes.AddRangeAsync(filme);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllAsync()
    {
        await _context.Filmes.ExecuteDeleteAsync();
    }
}