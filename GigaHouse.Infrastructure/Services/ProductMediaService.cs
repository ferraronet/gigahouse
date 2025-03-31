using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Services
{
    public class ProductMediaService : IProductMediaService
    {
        private readonly IProductMediaRepository _productMediaRepository;
        private readonly IProductRepository _productRepository;

        public ProductMediaService(IProductMediaRepository productMediaRepository, IProductRepository productRepository)
        {
            _productMediaRepository = productMediaRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductMedia> CreateAsync(ProductMedia item, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product != null)
            {
                var itemRepository = await _productMediaRepository.GetByIdAsync(item.Id);

                if (itemRepository == null)
                {
                    item.Product = product;
                    item.CreatedAt = DateTime.Now;
                    await _productMediaRepository.CreateAsync(item, cancellationToken);
                }
                else
                {
                    throw new Exception("Media already exists");
                }

                return item; 
            }
            else
            {
                throw new Exception("Product not exists");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productMediaRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _productMediaRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<ProductMedia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _productMediaRepository.GetByIdAsync(id);
            return itemRepository;
        }

        public async Task<PaginatedList<ProductMedia>> GetAllByProductId(Guid productId, CancellationToken cancellationToken = default)
        {
            var productMedias = await _productMediaRepository.GetPaginatedAsync(pageNumber: 1, pageSize: int.MaxValue, filter: query =>
            {
                return query.Where(e => EF.Property<Guid>(e, "ProductId") == productId);
            });

            return productMedias;
        }
    }
}
