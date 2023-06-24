namespace ApiWebFilme.EntityConfiguration;

public class FilmesEntityTypeConfiguration : IEntityTypeConfiguration<Filme>
{
    public void Configure(EntityTypeBuilder<Filme> builder)
    {
        builder
            .ToTable("Filmes");

        builder
            .HasKey(f => f.Id);

        builder
            .Property(f => f.Id)
            .ValueGeneratedNever();

        builder
            .Property(f => f.Title)
            .IsRequired();

        builder
            .Property(f => f.Year)
            .IsRequired();

        builder
            .Property(f => f.Studios)
            .IsRequired();

        builder
            .Property(f => f.Winner);
    }
}
