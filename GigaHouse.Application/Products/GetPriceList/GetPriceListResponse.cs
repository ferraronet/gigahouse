using GigaHouse.Infrastructure.Models.Caches;
using MongoDB.Bson.Serialization.Attributes;

namespace GigaHouse.Application.Products.GetPriceList;

public class GetPriceListResponse
{
    public string Gtin { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    
    public List<UserProductPriceResponse> ProductPrices { get; set; } = new List<UserProductPriceResponse>();
}

public class UserProductPriceResponse
{
    public decimal? ProductPrice { get; set; }

    public int? Installments { get; set; }

    public string Link { get; set; } = string.Empty;

    public decimal? InstallmentPrice { get; set; }

    public DateTime LastDateSearch { get; set; }
}
