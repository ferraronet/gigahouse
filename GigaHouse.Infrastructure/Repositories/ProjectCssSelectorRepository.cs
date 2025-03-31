using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class ProjectCssSelectorRepository : BaseRepository<ProjectCssSelector>, IProjectCssSelectorRepository
    {
        public ProjectCssSelectorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
