using ApiWebFilme.Controllers;
using ApiWebFilme.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TestApiWebFilmeApplication
{
    [TestFixture]
    public class FilmesControllerIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task ObterPremios_DeveRetornarStatusCode200()
        {
            // Arrange && Act
            var response = await _client.GetAsync("/v1/api/filmes/premios");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
