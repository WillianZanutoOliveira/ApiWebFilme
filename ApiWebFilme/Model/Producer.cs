namespace ApiWebFilme.Model;

public class Producer
{
    public const int NameLength = 500;

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Guid FilmeId { get; private set; }

    public Filme Filme { get; init; } = null!;

    public Producer(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
