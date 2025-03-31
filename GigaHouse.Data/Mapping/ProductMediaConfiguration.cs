using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Data.Mapping
{
    public class ProductMediaConfiguration : IEntityTypeConfiguration<ProductMedia>
    {
        public void Configure(EntityTypeBuilder<ProductMedia> builder)
        {
            builder.ToTable("ProductMedias");

            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Link)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pm => pm.Type)
                .HasMaxLength(50);

            builder.Property(pm => pm.ProductId)
                .IsRequired();

            builder.HasOne(t => t.Product)
               .WithMany(t=> t.ProductMedias)
               .HasForeignKey(t => t.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
