namespace TestApiWebFilmeApplication;

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

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var premiosResponse = await response.Content.ReadFromJsonAsync<ObterPremioViewModel>();
        
        Assert.IsNotNull(premiosResponse);
        Assert.That(1, Is.EqualTo(premiosResponse.Min.Count()));
        Assert.That(1, Is.EqualTo(premiosResponse.Max.Count()));

        var minPremio = premiosResponse.Min.FirstOrDefault();
        var maxPremio = premiosResponse.Max.FirstOrDefault();

        Assert.That(new ProducerViewModel 
        { 
            Producer =  "JOEL SILVER",
            Interval = 1,
            PreviousWin = 1990,
            FollowingWin = 1991
        }, Is.EqualTo(minPremio));

        Assert.That(new ProducerViewModel
        {
            Producer = "MATTHEW VAUGHN",
            Interval = 13,
            PreviousWin = 2002,
            FollowingWin = 2015
        }, Is.EqualTo(maxPremio));
    }

}
