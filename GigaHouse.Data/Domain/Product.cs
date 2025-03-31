using GigaHouse.Core.Enums;
using GigaHouse.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaHouse.Data.Domain
{
    public class Product : BaseEntity
    {
        public string Gtin { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        [Column(TypeName = "TEXT")]
        public string FullDescription { get; set; }

        public List<ProductMedia> ProductMedias { get; set; } = new List<ProductMedia>();

        public ProductStatus Status { get; set; }
    }
}
