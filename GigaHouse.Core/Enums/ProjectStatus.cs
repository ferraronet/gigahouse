using System.ComponentModel.DataAnnotations;

namespace GigaHouse.Core.Enums
{
    public enum ProjectStatus
    {
        [Display(Name = "NotStarted")]
        NotStarted = 0,
        [Display(Name = "InProgress")]
        InProgress = 1,
        [Display(Name = "Completed")]
        Completed = 2,
        [Display(Name = "OnHold")]
        OnHold = 3,
        [Display(Name = "Cancelled")]
        Cancelled = 4,
        [Display(Name = "Archived")]
        Archived = 5
    }
}
