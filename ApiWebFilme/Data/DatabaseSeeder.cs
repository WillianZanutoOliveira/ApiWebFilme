namespace ApiWebFilme.Data;

public sealed class DatabaseSeeder(
    FilmesContext context,
    IFilmesRepository filmesRepository,
    IHostEnvironment hostEnvironment)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await context.Filmes.AnyAsync(cancellationToken))
        {
            return;
        }

        var solutionDirectory = Path.GetDirectoryName(hostEnvironment.ContentRootPath)
            ?? hostEnvironment.ContentRootPath;

        var filePath = Path.Combine(solutionDirectory, "Assets", "movielist.csv");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                "The Golden Raspberry Awards dataset was not found.",
                filePath);
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);

        var movieDtos = csv.GetRecords<FilmesDto>().ToList();

        var movies = movieDtos
            .Select(movie => new Filme(
                year: movie.year,
                title: movie.title,
                studios: movie.studios,
                producers: movie.producers,
                winner: movie.winner))
            .ToList();

        await filmesRepository.CreateAsync(movies);
    }
}
