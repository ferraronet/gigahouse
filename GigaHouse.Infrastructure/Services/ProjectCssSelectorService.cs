using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Services
{
    public class ProjectCssSelectorService : IProjectCssSelectorService
    {
        private readonly IProjectCssSelectorRepository _projectCssSelectorRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectCssSelectorService(IProjectCssSelectorRepository projectCssSelectorRepository, IProjectRepository projectRepository)
        {
            _projectCssSelectorRepository = projectCssSelectorRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectCssSelector> CreateAsync(ProjectCssSelector item, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetByIdAsync(item.ProjectId);

            if (true)
            {
                var itemRepository = await _projectCssSelectorRepository.GetByIdAsync(item.Id);

                if (itemRepository == null)
                {
                    item.Project = project;
                    item.CreatedAt = DateTime.Now;
                    await _projectCssSelectorRepository.CreateAsync(item, cancellationToken);
                }
                else
                {
                    throw new Exception("ProjectCssSelector already exists");
                }

                return item; 
            }
            else
            {
                throw new Exception("Project not exists");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectCssSelectorRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _projectCssSelectorRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<ProjectCssSelector?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var listItems = await _projectCssSelectorRepository.GetAllAsync();
            return listItems.FirstOrDefault(f => f.ProjectId == projectId);
        }

        public async Task<ProjectCssSelector?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectCssSelectorRepository.GetByIdAsync(id);
            return itemRepository;
        }

        public async Task<PaginatedList<ProjectCssSelector>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var projectCssSelectors = await _projectCssSelectorRepository.GetPaginatedAsync(pageNumber: 1, pageSize: int.MaxValue, filter: query =>
            {
                return query.Where(e => EF.Property<Guid>(e, "ProjectId") == projectId);
            });

            return projectCssSelectors;
        }

        public async Task<ProjectCssSelector?> UpdateAsync(ProjectCssSelector item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectCssSelectorRepository.GetByIdAsync(item.Id);

            if (itemRepository != null)
            {
                itemRepository.ProductPrice = item.ProductPrice;
                itemRepository.Installments = item.Installments;
                itemRepository.InstallmentPrice = item.InstallmentPrice;
                itemRepository.ShippingPrice = item.ShippingPrice;
                itemRepository.Vendor = item.Vendor;
                itemRepository.VendorUnits = item.VendorUnits;
                itemRepository.Rating = item.Rating;
                itemRepository.RatingCount = item.RatingCount;
                itemRepository.Thermometer = item.Thermometer;
                itemRepository.Announcement = item.Announcement;
                itemRepository.Coupon = item.Coupon;
                itemRepository.FreeShipping = item.FreeShipping;
                itemRepository.IsFull = item.IsFull;
                itemRepository.BreadCrumb = item.BreadCrumb;
                itemRepository.UpdatedAt = DateTime.Now;

                await _projectCssSelectorRepository.UpdateAsync(itemRepository, cancellationToken);
            }

            return itemRepository;
        }
    }
}
