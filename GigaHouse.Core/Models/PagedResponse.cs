//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GigaHouse.Core.Models
//{
//    public class PagedResponse<T>
//    {
//        public IEnumerable<T> Items { get; set; } = new List<T>();

//        public int TotalCount { get; set; }

//        public int PageNumber { get; set; }

//        public int PageSize { get; set; }

//        public int TotalPages { get; set; }

//        public bool HasPrevious => PageNumber > 1;

//        public bool HasNext => PageNumber < TotalPages;
//    }
//}
