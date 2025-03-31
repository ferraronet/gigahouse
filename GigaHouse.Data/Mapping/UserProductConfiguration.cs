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
    public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
            builder.ToTable("UserProducts");

            builder.HasKey(up => up.Id);

            builder.Property(up => up.UserId)
                .IsRequired();

            builder.Property(up => up.ProductId)
                .IsRequired();

            builder.Property(up => up.TaskId)
                .IsRequired(false);

            builder.Property(t => t.StartedAt)
                .IsRequired();

            builder.Property(t => t.FinishedAt)
                .IsRequired(false);

            builder.HasIndex(up => up.UserId);
            builder.HasIndex(up => up.ProductId);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Product)
                .WithMany()
                .HasForeignKey(up => up.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Task)
                .WithMany()
                .HasForeignKey(up => up.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
