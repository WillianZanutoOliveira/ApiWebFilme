using ApiWebFilme.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiWebFilme.Repositories
{
    public class FilmesRepository : IFilmesRepository
    {
        public readonly FilmesContext _context;

        public FilmesRepository(FilmesContext context)
        {
            _context = context;
        }

        public async Task Create(List<Filmes> filme)
        {
            if (!_context.Filme.Any())
            {
                await _context.Filme.AddRangeAsync(filme);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Filmes>> Get()
        {
            return await _context.Filme.ToListAsync();
        }

    }

}

