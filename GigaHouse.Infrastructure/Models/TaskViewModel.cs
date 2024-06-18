using System.ComponentModel.DataAnnotations;

namespace GigaHouse.Infrastructure.Models
{
    public class TaskViewModel
    {
        private int _status;

        public int Id { get; set; }

        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(maximumLength: 2048)]
        public string? Description { get; set; }

        public int ProjectId { get; set; }

        public int Status
        {
            set
            {
                _status = value;
            }
        }

        public string StatusTask => ((Core.Enums.TaskStatus)_status).ToString();

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
