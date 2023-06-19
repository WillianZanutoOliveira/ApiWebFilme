using ApiWebFilme.Model;
using ApiWebFilme.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Globalization;


namespace ApiWebFilme.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmesRepository _filmeRepository;

        public FilmesController(IFilmesRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        [HttpGet("premios")]
        public async Task<IActionResult> ObterPremios()
        {
            try
            {
                string diretorioBase = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                string caminhoArquivo = Path.Combine(diretorioBase, "ApiWebFilme", "Content", "movielist.csv");

                var fileProvider = new PhysicalFileProvider(diretorioBase);
                var fileInfo = fileProvider.GetFileInfo("ApiWebFilme/ApiWebFilme/Content/movielist.csv");

                if (fileInfo.Exists)
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                    };

                    using (var reader = new StreamReader(fileInfo.CreateReadStream()))
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Read();
                        csv.ReadHeader();

                        var filmes = csv.GetRecords<Filmes>().ToList();

                        await _filmeRepository.Create(filmes);
                    }

                    var retorno = await _filmeRepository.Get();

                    var winners = retorno
                   .Where(m => m.winner == "yes")
                   .OrderBy(m => m.year)
                   .ToList();

                    var producers = new List<Produtor>();

                    for (int i = 0; i < winners.Count - 1; i++)
                    {
                        var current = winners[i];
                        var next = winners[i + 1];

                        var interval = next.year - current.year;

                        var producer = producers.FirstOrDefault(p => p.Producer == current.producers);

                        if (producer == null)
                        {
                            producer = new Produtor
                            {
                                Producer = current.producers,
                                Interval = interval,
                                PreviousWin = current.year,
                                FollowingWin = next.year
                            };

                            producers.Add(producer);
                        }
                        else if (interval < producer.Interval)
                        {
                            producer.Interval = interval;
                            producer.PreviousWin = current.year;
                            producer.FollowingWin = next.year;
                        }
                    }

                    var result = new
                    {
                        min = producers.OrderBy(p => p.Interval).Take(2),
                        max = producers.OrderByDescending(p => p.Interval).Take(2)
                    };

                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

               
            }
            catch (Exception e)
            {
                throw e.GetBaseException();
            }
            
        }

    }
}
