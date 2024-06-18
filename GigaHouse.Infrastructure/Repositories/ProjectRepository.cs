using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
