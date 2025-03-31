using GigaHouse.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Data.Domain
{
    public class TaskHistory : BaseEntity
    {
        public decimal? ProductPrice { get; set; }

        public int? Installments { get; set; }

        public decimal? InstallmentPrice { get; set; }

        public decimal? ShippingPrice { get; set; }

        public string Vendor { get; set; } = string.Empty;

        public int? VendorUnits { get; set; }

        public double? Rating { get; set; }

        public string RatingCount { get; set; } = string.Empty;

        public string Thermometer { get; set; } = string.Empty;

        public string Announcement { get; set; } = string.Empty;

        public string Coupon { get; set; } = string.Empty;

        public bool? IsFreeShipping { get; set; }

        public bool? IsFull { get; set; }

        public string BreadCrumb { get; set; } = string.Empty;


        public Guid ProjectId { get; set; }


        public Guid TaskId { get; set; }

        public Project Project { get; set; } = new Project();

        public Task Task { get; set; } = new Task();
    }
}
