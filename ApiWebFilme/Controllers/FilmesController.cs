namespace ApiWebFilme.Controllers;

[Route("v1/api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly IFilmesRepository _filmeRepository;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IObterPremiosRepository _obterPremiosRepository;

    public FilmesController(
        IFilmesRepository filmeRepository,
        IHostEnvironment hostEnvironment,
        IObterPremiosRepository obterPremiosRepository
    )
    {
        _filmeRepository = filmeRepository;
        _hostEnvironment = hostEnvironment;
        _obterPremiosRepository = obterPremiosRepository;
    }

    [HttpGet("premios")]
    [ProducesResponseType(typeof(ObterPremioViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<ObterPremioViewModel>> ObterPremios()
    {
        await InitializeDatabase();

        var obterPremio = await _obterPremiosRepository.GetAsync();

        return Ok(obterPremio);
    }

    private async Task InitializeDatabase()
    {
        await _filmeRepository.DeleteAllAsync();
        string solutionDirectory = Path.GetDirectoryName(_hostEnvironment.ContentRootPath);
        var assetsPath = Path.Combine(solutionDirectory, "Assets");
        string filePath = Path.Combine(assetsPath, "movielist.csv");

        if (System.IO.File.Exists(filePath))
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                var filmeDto = csv.GetRecords<FilmesDto>().ToList();
                var filme = filmeDto.Select(f => new Filme(
                    year: f.year,
                    title: f.title,
                    studios: f.studios,
                    producers: f.producers,
                    winner: f.winner
                )).ToList();
                await _filmeRepository.CreateAsync(filme);
            }

        }

    }

}
