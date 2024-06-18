using System.ComponentModel.DataAnnotations;

namespace GigaHouse.TaskList.Models
{
    public class TaskSaveModel
    {
        [Required, StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int ProjectId { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
