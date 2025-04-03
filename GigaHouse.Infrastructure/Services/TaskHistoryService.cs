using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static OpenQA.Selenium.PrintOptions;

namespace GigaHouse.Infrastructure.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public TaskHistoryService(ITaskRepository taskRepository, ITaskHistoryRepository taskHistoryRepository)
        {
            _taskRepository = taskRepository;
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task<TaskHistory> CreateAsync(TaskHistory item, CancellationToken cancellationToken = default)
        {
            var task = await _taskRepository.GetByIdAsync(item.TaskId, f => f.Product, f => f.Project);

            if (task != null)
            {
                item.Id = Guid.NewGuid();
                item.Task = task;
                item.Project = task.Project;
                await _taskHistoryRepository.CreateAsync(item, cancellationToken);

                return item;
            }
            else
            {
                throw new Exception("Task not exists");
            }
        }

        public async Task<TaskHistory?> GetLastHistoryTaskByTaskIdAsync(Guid taskId, decimal price, CancellationToken cancellationToken = default)
        {
            var taskHistories = await _taskHistoryRepository.GetAllAsync(
                filter: query => query.Where(th => th.ProductPrice == price && th.TaskId == taskId)
                );

            return taskHistories.GroupBy(th => new { th.TaskId, th.ProductPrice, th.ProjectId })
                                .Select(g => new TaskHistory()
                                {
                                    ProductPrice = g.Key.ProductPrice,
                                    Installments = g.Max(th => th.Installments),
                                    InstallmentPrice = g.Max(th => th.InstallmentPrice),
                                    ShippingPrice = g.Max(th => th.ShippingPrice),
                                    Vendor = g.Max(th => th.Vendor),
                                    VendorUnits = g.Max(th => th.VendorUnits),
                                    Rating = g.Max(th => th.Rating),
                                    RatingCount = g.Max(th => th.RatingCount),
                                    Thermometer = g.Max(th => th.Thermometer),
                                    Announcement = g.Max(th => th.Announcement),
                                    Coupon = g.Max(th => th.Coupon),
                                    IsFreeShipping = g.Max(th => th.IsFreeShipping),
                                    IsFull = g.Max(th => th.IsFull),
                                    BreadCrumb = g.Max(th => th.BreadCrumb),
                                    ProjectId = g.Key.ProjectId,
                                    TaskId = g.Key.TaskId,
                                    CreatedAt = g.Max(th => th.CreatedAt),
                                    UpdatedAt = g.Max(th => th.UpdatedAt)
                                })
                                .FirstOrDefault();
        }
    }
}
