using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace GigaHouse.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductMedia> ProductMedias { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectProduct> ProjectProducts { get; set; }

        public DbSet<Domain.Task> Tasks { get; set; }

        public DbSet<ProjectCssSelector> ProjectCssSelectors { get; set; }

        public DbSet<TaskHistory> TaskHistories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
