using System.ComponentModel.DataAnnotations;

namespace GigaHouse.TaskList.Models
{
    public class ProjectSaveModel
    {
        [Required, StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
    }
}
