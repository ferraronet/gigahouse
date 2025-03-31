using GigaHouse.Data.Common;

namespace GigaHouse.Data.Domain
{
    public class Task : BaseEntity
    {
        public string Link { get; set; } = string.Empty;

        public int TimesPerDay { get; set; }

        public Core.Enums.TaskStatus Status { get; set; }

        public DateTime LastDateSearch { get; set; }

        public Guid? WorkerId { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProductId { get; set; }

        public Project Project { get; set; } = new Project();

        public Product Product { get; set; } = new Product();
    }
}
