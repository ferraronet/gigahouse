using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Repositories;

public class UserProductRepository : BaseRepository<UserProduct>, IUserProductRepository
{
    public UserProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
