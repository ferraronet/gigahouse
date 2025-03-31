using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Infrastructure.Models.Caches
{
    public class UserProductLog
    {
        [BsonId]
        public string Id { get; set; }

        public string Gtin { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<UserProductPriceLog> ProductPrices { get; set; } = new List<UserProductPriceLog>();       
    }

    public class UserProductPriceLog
    {
        [BsonId]
        public string Id { get; set; }

        public decimal? ProductPrice { get; set; }

        public string Link { get; set; } = string.Empty;

        public int? Installments { get; set; }

        public decimal? InstallmentPrice { get; set; }

        public DateTime LastDateSearch { get; set; }
    }
}
