using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace GigaHouse.Data.Mapping
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Link)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.TimesPerDay)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.LastDateSearch)
                .IsRequired();

            builder.Property(t => t.WorkerId)
                .IsRequired(false);

            builder.Property(t => t.ProjectId)
                .IsRequired();

            builder.Property(t => t.ProductId)
                .IsRequired();

            builder.HasIndex(t => t.ProjectId);
            builder.HasIndex(t => t.ProductId);

            builder.HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Product)
                .WithMany()
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
