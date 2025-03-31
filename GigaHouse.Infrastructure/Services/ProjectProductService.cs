using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Services
{
    public class ProjectProductService : IProjectProductService
    {
        private readonly IProjectProductRepository _projectProductRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProductRepository _productRepository;

        public ProjectProductService(IProjectProductRepository projectProductRepository, IProjectRepository projectRepository, IProductRepository productRepository)
        {
            _projectProductRepository = projectProductRepository;
            _projectRepository = projectRepository;
            _productRepository = productRepository;
        }

        public async Task<ProjectProduct> CreateAsync(ProjectProduct item, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            var project = await _projectRepository.GetByIdAsync(item.ProjectId);

            if (product != null && project != null)
            {
                var itemRepository = await _projectProductRepository.GetByIdAsync(item.Id);

                if (itemRepository == null)
                {
                    item.Product = product;
                    item.Project = project;
                    item.CreatedAt = DateTime.Now;
                    await _projectProductRepository.CreateAsync(item, cancellationToken);
                }
                else
                {
                    throw new Exception("Product already exists");
                }

                return item; 
            }
            else
            {
                throw new Exception("Product or Project not exists");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectProductRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _projectProductRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<ProjectProduct?> GetByProjectIdAndProductIdAsync(Guid projectId, Guid productId, CancellationToken cancellationToken = default)
        {
            var listItems = await _projectProductRepository.GetAllAsync();
            return listItems.FirstOrDefault(f => f.ProjectId == projectId && f.ProductId == productId);
        }

        public async Task<ProjectProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectProductRepository.GetByIdAsync(id);
            return itemRepository;
        }

        public async Task<PaginatedList<ProjectProduct>> GetAllByProjectIdAndProductId(Guid projectId, Guid productId, CancellationToken cancellationToken = default)
        {
            var projectProducts = await _projectProductRepository.GetPaginatedAsync(pageNumber: 1, pageSize: int.MaxValue, filter: query =>
            {
                return query.Where(e => EF.Property<Guid>(e, "ProjectId") == projectId && EF.Property<Guid>(e, "ProductId") == productId);
            });

            return projectProducts;
        }

        public async Task<ProjectProduct?> UpdateAsync(ProjectProduct item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectProductRepository.GetByIdAsync(item.Id);

            if (itemRepository != null)
            {
                itemRepository.MetaKeywords = item.MetaKeywords;
                itemRepository.MetaTitle = item.MetaTitle;
                itemRepository.MetaDescription = item.MetaDescription;
                itemRepository.UpdatedAt = DateTime.Now;

                await _projectProductRepository.UpdateAsync(itemRepository, cancellationToken);
            }

            return itemRepository;
        }
    }
}
