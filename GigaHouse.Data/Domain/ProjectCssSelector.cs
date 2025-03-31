using GigaHouse.Data.Common;

namespace GigaHouse.Data.Domain
{
    public class ProjectCssSelector : BaseEntity
    {
        public string ProductPrice { get; set; }

        public string Installments { get; set; }

        public string InstallmentPrice { get; set; }

        public string ShippingPrice { get; set; }

        public string Vendor { get; set; }

        public string VendorUnits { get; set; }

        public string Rating { get; set; }

        public string RatingCount { get; set; }

        public string Thermometer { get; set; }

        public string Announcement { get; set; }

        public string Coupon { get; set; }

        public string FreeShipping { get; set; }

        public string IsFull { get; set; }

        public string BreadCrumb { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; } = new Project();
    }
}
