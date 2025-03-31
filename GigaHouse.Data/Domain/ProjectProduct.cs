using GigaHouse.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Data.Domain
{
    public class ProjectProduct : BaseEntity
    {
        public string MetaKeywords { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public Guid ProjectId { get; set; }

        public Guid ProductId { get; set; }

        public Project Project { get; set; } = new Project();

        public Product Product { get; set; } = new Product();
    }
}
