using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Models.Caches;
using GigaHouse.Infrastructure.MongoDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using Serilog.Debugging;

namespace GigaHouse.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRequestLogService _requestLogService;
        private readonly IUserProductService _userProductService;

        public ProductService(IProductRepository productRepository, IRequestLogService requestLogService, IUserProductService userProductService)
        {
            _productRepository = productRepository;
            _requestLogService = requestLogService;
            _userProductService = userProductService;
        }

        public async Task<Product> CreateAsync(Product item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productRepository.GetByIdAsync(item.Id);

            if (itemRepository == null)
            {
                item.CreatedAt = DateTime.Now;
                await _productRepository.CreateAsync(item, cancellationToken);
            }
            else
            {
                throw new Exception("Product already exists");
            }

            return item;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _productRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<Product?> GetByGtinAsync(string gtin, CancellationToken cancellationToken = default)
        {
            var listItems = await _productRepository.GetAllAsync();
            return listItems.FirstOrDefault(f => f.Gtin == gtin);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productRepository.GetByIdAsync(id);
            return itemRepository;
        }

        public async Task<PaginatedList<Product>> GetAllProducts(int pageNumber = 1, int pageSize = 10, string? gtin = null, string? name = null, ProductStatus? status = null, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetPaginatedAsync(pageNumber: pageNumber, pageSize: pageSize, filter: query =>
            {
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name").Trim().ToUpper(), $"%{name.Trim().ToUpper()}%"));

                if (!string.IsNullOrEmpty(gtin))
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Gtin").Trim().ToUpper(), $"%{gtin.Trim().ToUpper()}%"));

                if (status != null)
                    query = query.Where(e => EF.Property<int>(e, "Status") == (int)status);

                return query;
            });

            return products;
        }


        public async Task<UserProductLog> GetProductPricesByUserIdAsync(Guid userId = default, Guid productId = default, CancellationToken cancellationToken = default)
        {
            var userProduct = await _userProductService.GetByUserIdAndProductIdAndTaskIdAsync(userId, productId);

            if(userProduct == null)
                throw new KeyNotFoundException("Product not registered for this user");

            var requestLog = await _requestLogService.GetRequestByIdAsync(productId.ToString());

            if (requestLog == null)
                throw new KeyNotFoundException("Product not found");

            var log = BsonSerializer.Deserialize<UserProductLog>(requestLog.RequestBody);

            if (log == null)
                throw new KeyNotFoundException("Product not found");

            var listLog = log.ProductPrices.Where(f => f.LastDateSearch >= userProduct.StartedAt);

            if (userProduct.FinishedAt != null)
                listLog = listLog.Where(f => f.LastDateSearch <= userProduct.FinishedAt);

            log.ProductPrices = listLog.ToList();

            return log;
        }


        public async Task<Product?> UpdateAsync(Product item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productRepository.GetByIdAsync(item.Id);

            if (itemRepository != null)
            {
                itemRepository.Name = item.Name;
                itemRepository.Gtin = item.Gtin;
                itemRepository.ShortDescription = item.ShortDescription;
                itemRepository.FullDescription = item.FullDescription;
                itemRepository.Status = item.Status;
                itemRepository.UpdatedAt = DateTime.Now;

                await _productRepository.UpdateAsync(itemRepository, cancellationToken);
            }

            return itemRepository;
        }
    }
}
