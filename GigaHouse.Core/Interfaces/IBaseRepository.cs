using GigaHouse.Core.Models;

namespace GigaHouse.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<PaginatedDataViewModel<T>> GetPaginatedDataAsync(int pageNumber, int pageSize);

        Task<T> GetByIdAsync<Tid>(Tid id);

        Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value);

        Task<bool> IsExistsAsync<Tvalue>(string key, Tvalue value, int id, string parameter);

        Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value);

        Task<bool> IsExistsForUpdateAsync<Tid>(Tid id, string key, string value, int idParameter, string parameter);

        Task<T> CreateAsync(T model);

        Task UpdateAsync(T model);

        Task DeleteAsync(T model);

        Task SaveChangeAsync();
    }
}
