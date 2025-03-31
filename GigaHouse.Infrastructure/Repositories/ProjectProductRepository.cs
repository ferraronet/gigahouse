using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class ProjectProductRepository : BaseRepository<ProjectProduct>, IProjectProductRepository
    {
        public ProjectProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
