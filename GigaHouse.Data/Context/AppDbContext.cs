using GigaHouse.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Task> Task { get; set; }

        public DbSet<Project> Project { get; set; }
    }
}
