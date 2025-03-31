using GigaHouse.Data.Interfaces;
using GigaHouse.Core.Models;
using GigaHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<PaginatedList<T>> GetPaginatedAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

            if (filter != null)
                query = filter(query);

            return await PaginatedList<T>.CreateAsync(query, pageNumber, pageSize, orderBy);
        }

        public async Task<IEnumerable<T>> GetBySqlQueryAsync<T>(string sqlQuery, params object[] parameters) where T : class
        {
            return await _dbContext.Set<T>()
                                   .FromSqlRaw(sqlQuery, parameters)
                                   .AsNoTracking()
                                   .ToListAsync();
        }
        public async Task<PaginatedList<T>> GetPaginatedBySqlQueryAsync<T>(string sqlQuery, int pageNumber, int pageSize, params object[] parameters) where T : class
        {
            var query = _dbContext.Set<T>()
                                        .FromSqlRaw(sqlQuery, parameters)
                                        .AsNoTracking();

            return await PaginatedList<T>.CreateAsync(query, pageNumber, pageSize);
        }

        //public async Task<T> GetByIdAsync<Tid>(Tid id)
        //{
        //    var data = await _dbContext.Set<T>().FindAsync(id);            
        //    return data;
        //}

        public async Task<T> GetByIdAsync<TId>(TId id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(e => EF.Property<TId>(e, "Id")!.Equals(id));
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

        public async Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value, Guid id, string parameterId)
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

        public async Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value, Guid idParameter, string parameterId)
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


        public async Task<T> CreateAsync(T model, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(model, cancellationToken);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task UpdateAsync(T model, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(model);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T model, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(model);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
