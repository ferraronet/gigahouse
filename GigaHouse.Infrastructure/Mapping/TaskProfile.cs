using AutoMapper;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Data.Domain.Task, TaskViewModel>().ReverseMap();
        }
    }
}
