using GigaHouse.Infrastructure.Models.Caches;

namespace GigaHouse.Application.Products.GetPriceList;

public class GetPriceListResult
{
    public string Gtin { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<UserProductPriceResult> ProductPrices { get; set; } = new List<UserProductPriceResult>();
}

public class UserProductPriceResult
{
    public decimal? ProductPrice { get; set; }

    public int? Installments { get; set; }

    public string Link { get; set; } = string.Empty;

    public decimal? InstallmentPrice { get; set; }

    public DateTime LastDateSearch { get; set; }
}
