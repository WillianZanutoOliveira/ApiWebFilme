namespace ApiWebFilme.Repositories;

public interface IObterPremiosRepository
{
    Task<ObterPremioViewModel> GetAsync();
}
