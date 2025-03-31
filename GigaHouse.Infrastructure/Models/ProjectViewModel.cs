//using GigaHouse.Core.Enums;
//using System.ComponentModel.DataAnnotations;

//namespace GigaHouse.Infrastructure.Models
//{
//    public class ProjectViewModel
//    {
//        private int _status;

//        public ProjectViewModel()
//        {
//            Tasks = new List<TaskViewModel>();
//        }

//        public Guid Id { get; set; }

//        [Required, StringLength(maximumLength: 100, MinimumLength = 2)]
//        public string Name { get; set; } = string.Empty;

//        [Required, StringLength(maximumLength: 100, MinimumLength = 5)]
//        public string Link { get; set; } = string.Empty;

//        public List<TaskViewModel> Tasks { get; set; }

//        //public int Status {
//        //    set
//        //    {
//        //        _status = value;
//        //    }
//        //}

//        public string StatusProject => ((ProjectStatus)_status).ToString();
//    }
//}
