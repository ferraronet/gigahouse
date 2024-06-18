using System.ComponentModel.DataAnnotations;

namespace GigaHouse.Core.Enums
{
    public enum TaskStatus
    {
        [Display(Name = "ToDo")]
        ToDo = 0,
        [Display(Name = "Doing")]
        Doing = 1,
        [Display(Name = "Testing")]
        Testing = 2,
        [Display(Name = "Done")]
        Done = 3
    }
}
