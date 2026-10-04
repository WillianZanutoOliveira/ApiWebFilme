public class FilmesContext : DbContext
{
    public FilmesContext(DbContextOptions<FilmesContext> options)
        : base(options)
    {
    }

    public DbSet<Filme> Filmes { get; set; }

    public DbSet<Producer> Producers { get; set; }
}
