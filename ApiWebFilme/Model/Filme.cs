namespace ApiWebFilme.Model;

public class Filme
{
    public Guid Id { get; private set; }

    public int Year { get; private set; }
    public string Title { get; private set; }
    public string Studios { get; private set; }
    public string? Winner { get; private set; }

    private readonly List<Producer> _producer = new();
    public IReadOnlyCollection<Producer> Producers => _producer;

    protected Filme() { }
    public Filme(int year, string title, string studios, string producers, string? winner)
    {
        Id = Guid.NewGuid();
        Year = year;
        Title = title;
        Studios = studios;
        Winner = winner;

        _producer.AddRange(ProcessProduter(producers));
    }

    public List<Producer> ProcessProduter(string producersCsv)
    {
        string input = producersCsv.Trim().ToUpper();

        string[] delimiters = { ",", " AND " };

        string[] producersRep = Regex.Split(input, string.Join("|", delimiters))
            .Select(name => name.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToArray();

        var producers = producersRep
            .Select(p => new Producer(p))
            .ToList();

        return producers;
    }
}