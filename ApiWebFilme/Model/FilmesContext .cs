using ApiWebFilme.Model;
using Microsoft.EntityFrameworkCore;

public class FilmesContext : DbContext
{
    public FilmesContext(DbContextOptions<FilmesContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Filmes> Filme { get; set; }
}