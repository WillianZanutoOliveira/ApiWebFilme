using ApiWebFilme.Model;
using ApiWebFilme.Repositories;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApiWebFilme.Controllers;
using Microsoft.Extensions.FileProviders;

namespace TestApiWebFilmeAplication
{
    public class Tests
    {
        /* [SetUp]
         public void Setup()
         {
         }

         [Test]
         public void Test1()
         {
             Assert.Pass();
         }*/

        [TestFixture]
        public class FilmesControllerTests
        {
            [Test]
            public async Task ObterPremios_DeveRetornarOk()
            {

                var filmeRepositoryMock = new Mock<IFilmesRepository>();
                var meuController = new FilmesController(filmeRepositoryMock.Object);
                var resultado = await meuController.ObterPremios();

                Assert.That(resultado, Is.TypeOf<OkObjectResult>());
            }

            [Test]
            public async Task LerArquivoCsv_DeveLerArquivoComSucesso()
            {
                var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                var arquivoCsvPath = "Content/movielist.csv";
                var fileInfo = fileProvider.GetFileInfo(arquivoCsvPath);


                Assert.That(fileInfo.Exists, Is.True);

                using (var streamReader = new StreamReader(fileInfo.PhysicalPath))
                {
                    var conteudo = streamReader.ReadToEnd();

                }
            }
        }
    }
}