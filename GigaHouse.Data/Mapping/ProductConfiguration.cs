using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Data.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Gtin)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.ShortDescription)
                .HasMaxLength(500);

            builder.Property(p => p.FullDescription)
                .HasColumnType("TEXT"); 

            builder.Property(p => p.Status).IsRequired();

            builder.HasMany(p => p.ProductMedias)
                .WithOne()
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Gtin).IsUnique();
        }
    }
}
