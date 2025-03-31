using GigaHouse.Core.Enums;

namespace GigaHouse.Application.ProjectCssSelectors.Update;

public class UpdateRequest
{
    public Guid Id { get; set; }

    public string ProductPrice { get; set; } = string.Empty;

    public string Installments { get; set; } = string.Empty;

    public string InstallmentPrice { get; set; } = string.Empty;

    public string ShippingPrice { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public string VendorUnits { get; set; } = string.Empty;

    public string Rating { get; set; } = string.Empty;

    public string RatingCount { get; set; } = string.Empty;

    public string Thermometer { get; set; } = string.Empty;

    public string Announcement { get; set; } = string.Empty;

    public string Coupon { get; set; } = string.Empty;

    public string FreeShipping { get; set; } = string.Empty;

    public string IsFull { get; set; } = string.Empty;

    public string BreadCrumb { get; set; } = string.Empty;
}