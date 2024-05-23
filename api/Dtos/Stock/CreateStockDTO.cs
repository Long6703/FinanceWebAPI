using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockDTO
    {

        [Required]
        [MaxLength(180, ErrorMessage = "Sysbol must be at most 180 characters")]
        public string Sysbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(180, ErrorMessage = "CompanyName must be at most 180 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(180, ErrorMessage = "Isdustry must be at most 180 characters")]
        public string Isdustry { get; set; } = string.Empty;

        [Range(1, 1000000)]
        public long MarketCap { get; set; }
    }
}