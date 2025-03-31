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
    public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            builder.ToTable("TaskHistories");

            builder.HasKey(th => th.Id);

            builder.Property(th => th.ProductPrice)
                .HasPrecision(18, 2)
                .IsRequired(false);

            builder.Property(th => th.Installments)
                .IsRequired(false);

            builder.Property(th => th.InstallmentPrice)
                .HasPrecision(18, 2)
                .IsRequired(false);

            builder.Property(th => th.ShippingPrice)
                .HasPrecision(18, 2)
                .IsRequired(false);

            builder.Property(th => th.Vendor)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(th => th.VendorUnits)
                .IsRequired(false);

            builder.Property(th => th.Rating)
                .IsRequired(false)
                .HasPrecision(18, 2); 

            builder.Property(th => th.RatingCount)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(th => th.Thermometer)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(th => th.Announcement)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(th => th.Coupon)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(th => th.IsFreeShipping)
                .IsRequired(false);

            builder.Property(th => th.IsFull)
                .IsRequired(false);

            builder.Property(th => th.BreadCrumb)
                .IsRequired(false)
                .HasMaxLength(4096);

            builder.Property(th => th.ProjectId)
                .IsRequired();

            builder.HasIndex(th => th.ProjectId);

            builder.HasOne(t => t.Project)
                  .WithMany()
                  .HasForeignKey(t => t.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
