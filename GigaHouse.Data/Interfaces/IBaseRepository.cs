using GigaHouse.Core.Models;
using System.Linq.Expressions;

namespace GigaHouse.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<PaginatedList<T>> GetPaginatedAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T> GetByIdAsync<Tid>(Tid id, params Expression<Func<T, object>>[] includes);

        Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value);

        Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value, Guid id, string parameter);

        Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value);

        Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value, Guid idParameter, string parameter);

        Task<T> CreateAsync(T model, CancellationToken cancellationToken = default);

        Task UpdateAsync(T model, CancellationToken cancellationToken = default);

        Task DeleteAsync(T model, CancellationToken cancellationToken = default);

        Task SaveChangeAsync(CancellationToken cancellationToken = default);

        Task<PaginatedList<T>> GetPaginatedBySqlQueryAsync<T>(string sqlQuery, int pageNumber, int pageSize, params object[] parameters) where T : class;
    }
}
