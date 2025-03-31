//using System.ComponentModel.DataAnnotations;

//namespace GigaHouse.Infrastructure.Models
//{
//    public class TaskViewModel
//    {
//        private int _status;

//        public Guid Id { get; set; }

//        [StringLength(maximumLength: 2048, MinimumLength = 5)]
//        public required string Link { get; set; }

//        public Guid ProjectId { get; set; }

//        public Guid ProductId { get; set; }

//        public Guid TaskCssSelectorId { get; set; }

//        //public int Status
//        //{
//        //    set
//        //    {
//        //        _status = value;
//        //    }
//        //}

//        public string StatusTask => ((Core.Enums.TaskStatus)_status).ToString();
//    }
//}
