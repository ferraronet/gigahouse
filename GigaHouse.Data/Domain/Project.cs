using GigaHouse.Core.Enums;
using GigaHouse.Data.Common;

namespace GigaHouse.Data.Domain
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        
        public string Link { get; set; }

        public ProjectStatus Status { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
