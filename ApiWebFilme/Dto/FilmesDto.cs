namespace ApiWebFilme.Dto;

public struct FilmesDto
{
    public int year { get; init; }
    public string title { get; init; }
    public string studios { get; init; }
    public string producers { get; init; }
    public string? winner { get; init; }
}
