namespace ApiWebFilme.Repositories;

public interface IFilmesRepository
{
    Task CreateAsync(List<Filme> filmes);
}
