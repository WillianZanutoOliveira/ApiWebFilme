using ApiWebFilme.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

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
            // Arrange
            var response = await _client.GetAsync("/v1/api/filmes/premios");
            response.EnsureSuccessStatusCode();

            // Act
            var premiosResponse = await response.Content.ReadFromJsonAsync<FilmeData>();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(premiosResponse);
            Assert.IsNotNull(premiosResponse.Min);
            Assert.AreEqual(2, premiosResponse.Min.Count);
            Assert.IsNotNull(premiosResponse.Max);
            Assert.AreEqual(2, premiosResponse.Max.Count);

            Assert.AreEqual("Gloria Katz", premiosResponse.Min[0].Producer);
            Assert.AreEqual(0, premiosResponse.Min[0].Interval);
            Assert.AreEqual(1986, premiosResponse.Min[0].PreviousWin);
            Assert.AreEqual(1986, premiosResponse.Min[0].FollowingWin);

            Assert.AreEqual("Steven Perry and Joel Silver", premiosResponse.Min[1].Producer);
            Assert.AreEqual(0, premiosResponse.Min[1].Interval);
            Assert.AreEqual(1990, premiosResponse.Min[1].PreviousWin);
            Assert.AreEqual(1990, premiosResponse.Min[1].FollowingWin);

            Assert.AreEqual("Mario Kassar, Joel B. Michaels and Andrew G. Vajna", premiosResponse.Max[0].Producer);
            Assert.AreEqual(2, premiosResponse.Max[0].Interval);
            Assert.AreEqual(2006, premiosResponse.Max[0].PreviousWin);
            Assert.AreEqual(2008, premiosResponse.Max[0].FollowingWin);

            Assert.AreEqual("Allan Carr", premiosResponse.Max[1].Producer);
            Assert.AreEqual(1, premiosResponse.Max[1].Interval);
            Assert.AreEqual(1980, premiosResponse.Max[1].PreviousWin);
            Assert.AreEqual(1981, premiosResponse.Max[1].FollowingWin);
        }

    }
}
