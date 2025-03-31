using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Services
{
    public class UserProductService : IUserProductService
    {
        private readonly IUserProductRepository _userProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public UserProductService(IUserProductRepository userProductRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _userProductRepository = userProductRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<UserProduct> CreateAsync(UserProduct item, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            var user = await _userRepository.GetByIdAsync(item.UserId);

            if (user != null && product != null)
            {
                var userRepository = await _userProductRepository.GetByIdAsync(item.Id);

                if (userRepository == null)
                {
                    item.User = user;
                    item.Product = product;
                    item.StartedAt = DateTime.Now;
                    item.CreatedAt = DateTime.Now;
                    await _userProductRepository.CreateAsync(item, cancellationToken);
                }
                else
                {
                    throw new Exception("UserProduct found");
                }

                return item;
            }
            else
            {
                throw new Exception("Product or User not exists");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userRepository = await _userProductRepository.GetByIdAsync(id);

            if (userRepository != null)
                await _userProductRepository.DeleteAsync(userRepository);
            else
                return false;

            return true;
        }

        public async Task<UserProduct?> GetByUserIdAndProductIdAndTaskIdAsync(Guid userId, Guid productId, Guid? taskId = default, CancellationToken cancellationToken = default)
        {
            var listUsers = await _userProductRepository.GetAllAsync();

            listUsers = listUsers.Where(f => f.UserId == userId && f.ProductId == productId);

            if (taskId != null)
                listUsers = listUsers.Where(f => f.TaskId == taskId);

            return listUsers.FirstOrDefault();
        }

        public async Task<UserProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userRepository = await _userProductRepository.GetByIdAsync(id);
            return userRepository;
        }

        public async Task<PaginatedList<UserProduct>> GetAllByUserIdAsync(int pageNumber, int pageSize, Guid userId, CancellationToken cancellationToken = default)
        {
            var projectProducts = await _userProductRepository.GetPaginatedAsync(
                pageNumber: pageNumber, 
                pageSize: pageSize, 
                filter: query => query.Where(e => EF.Property<Guid>(e, "UserId") == userId),
                orderBy: query => query.OrderByDescending(e => EF.Property<DateTime>(e, "StartedAt"))
                );

            return projectProducts;
        }
    }
}
