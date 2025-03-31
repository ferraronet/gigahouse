using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Data.Mapping
{
    public class ProjectProductConfiguration : IEntityTypeConfiguration<ProjectProduct>
    {
        public void Configure(EntityTypeBuilder<ProjectProduct> builder)
        {
            builder.ToTable("ProjectProducts");

            builder.HasKey(pp => pp.Id);

            builder.Property(pp => pp.MetaKeywords)
                .HasMaxLength(500);

            builder.Property(pp => pp.MetaTitle)
                .HasMaxLength(255);

            builder.Property(pp => pp.MetaDescription)
                .HasColumnType("TEXT");

            builder.Property(pp => pp.ProjectId)
                .IsRequired();

            builder.Property(pp => pp.ProductId)
                .IsRequired(); 

            builder.HasIndex(pp => pp.ProjectId);
            builder.HasIndex(pp => pp.ProductId);

            builder.HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Product)
                .WithMany()
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
