namespace ApiWebFilme.EntityConfiguration;

public class ProducersEntityTypeConfiguration : IEntityTypeConfiguration<Producer>
{
    public void Configure(EntityTypeBuilder<Producer> builder)
    {
        builder
            .ToTable("Producers");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Producer.NameLength);

        builder
            .HasOne(p => p.Filme)
            .WithMany(f => f.Producers)
            .HasForeignKey(p => p.FilmeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
