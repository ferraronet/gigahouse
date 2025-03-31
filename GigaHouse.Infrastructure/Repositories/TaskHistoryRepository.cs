using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class TaskHistoryRepository : BaseRepository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
