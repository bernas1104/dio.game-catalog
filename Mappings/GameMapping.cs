using DIO.GameCatalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIO.GameCatalog.Mappings
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nome");

            builder.Property(x => x.Produtora)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("produtora");

            builder.Property(x => x.Preco)
                .IsRequired()
                .HasPrecision(2)
                .HasColumnName("preco");
        }
    }
}
