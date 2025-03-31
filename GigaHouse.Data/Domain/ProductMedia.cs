using GigaHouse.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Data.Domain
{
    public class ProductMedia : BaseEntity
    {
        public string Link { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = new Product();
    }
}
