using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }

        public string Sysbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Isdustry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
    }
}