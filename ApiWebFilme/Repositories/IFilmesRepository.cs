using ApiWebFilme.Model;

namespace ApiWebFilme.Repositories
{
    public interface IFilmesRepository
    {
        Task<IEnumerable<Filmes>> Get();

        Task Create(List<Filmes> filme);

    }
}
