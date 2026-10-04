namespace ApiWebFilme.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly IObterPremiosRepository _obterPremiosRepository;

    public FilmesController(IObterPremiosRepository obterPremiosRepository)
    {
        _obterPremiosRepository = obterPremiosRepository;
    }

    [HttpGet("premios")]
    [ProducesResponseType(typeof(ObterPremioViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<ObterPremioViewModel>> ObterPremios()
    {
        var result = await _obterPremiosRepository.GetAsync();

        return Ok(result);
    }
}
