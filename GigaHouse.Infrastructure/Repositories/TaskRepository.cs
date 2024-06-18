using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<Data.Domain.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
