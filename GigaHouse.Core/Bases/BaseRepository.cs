using GigaHouse.Core.Interfaces;
using GigaHouse.Core.Models;
using GigaHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GigaHouse.Core.Bases
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<PaginatedDataViewModel<T>> GetPaginatedDataAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            var data = await query.ToListAsync();
            var totalCount = await _dbContext.Set<T>().CountAsync();

            return new PaginatedDataViewModel<T>(data, totalCount);
        }

        public async Task<T> GetByIdAsync<Tid>(Tid id)
        {
            var data = await _dbContext.Set<T>().FindAsync(id);
            if (data == null)
                throw new Exception("Id not found");
            return data;
        }

        public async Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }

        public async Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value, int id, string parameterId)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);

            var parameterIdProperty = Expression.Property(parameter, parameterId);
            var parameterIdConstant = Expression.Constant(id);
            var parameterIdEquality = Expression.Equal(parameterIdProperty, parameterIdConstant);

            var andExpression = Expression.AndAlso(equality, parameterIdEquality);

            var lambda = Expression.Lambda<Func<T, bool>>(andExpression, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }

        public async Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);

            var idProperty = Expression.Property(parameter, "Id");
            var idEquality = Expression.NotEqual(idProperty, Expression.Constant(id));

            var combinedExpression = Expression.AndAlso(equality, idEquality);
            var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }

        public async Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value, int idParameter, string parameterId)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, key);
            var constant = Expression.Constant(value);
            var equality = Expression.Equal(property, constant);

            var idProperty = Expression.Property(parameter, "Id");
            var idEquality = Expression.NotEqual(idProperty, Expression.Constant(id));

            var combinedExpression = Expression.AndAlso(equality, idEquality);

            var parameterIdProperty = Expression.Property(parameter, parameterId);
            var parameterIdConstant = Expression.Constant(idParameter);
            var parameterIdEquality = Expression.Equal(parameterIdProperty, parameterIdConstant);

            var andExpression = Expression.AndAlso(combinedExpression, parameterIdEquality);

            var lambda = Expression.Lambda<Func<T, bool>>(andExpression, parameter);

            return await _dbContext.Set<T>().AnyAsync(lambda);
        }


        public async Task<T> CreateAsync(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task UpdateAsync(T model)
        {
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T model)
        {
            _dbContext.Set<T>().Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
