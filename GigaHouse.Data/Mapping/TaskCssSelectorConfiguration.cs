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
    public class TaskCssSelectorConfiguration : IEntityTypeConfiguration<ProjectCssSelector>
    {
        public void Configure(EntityTypeBuilder<ProjectCssSelector> builder)
        {
            builder.ToTable("ProjectCssSelectors");

            builder.HasKey(tcs => tcs.Id);

            builder.Property(tcs => tcs.ProductPrice);
            builder.Property(tcs => tcs.Installments);
            builder.Property(tcs => tcs.InstallmentPrice);
            builder.Property(tcs => tcs.ShippingPrice);
            builder.Property(tcs => tcs.Vendor);
            builder.Property(tcs => tcs.VendorUnits);
            builder.Property(tcs => tcs.Rating);
            builder.Property(tcs => tcs.RatingCount);
            builder.Property(tcs => tcs.Thermometer);
            builder.Property(tcs => tcs.Announcement);
            builder.Property(tcs => tcs.Coupon);
            builder.Property(tcs => tcs.FreeShipping);
            builder.Property(tcs => tcs.IsFull);
            builder.Property(tcs => tcs.BreadCrumb);

            builder.Property(tcs => tcs.ProjectId)
                .IsRequired();

            builder.HasIndex(tcs => tcs.ProjectId);

            builder.HasOne(t => t.Project)
                 .WithMany()
                 .HasForeignKey(t => t.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
