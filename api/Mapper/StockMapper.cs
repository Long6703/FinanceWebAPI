using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static Dtos.Stock.StockDTO ToStockDTO(this Stock stock)
        {
            return new Dtos.Stock.StockDTO
            {
                Id = stock.Id,
                Sysbol = stock.Sysbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Isdustry = stock.Isdustry,
                MarketCap = stock.MarketCap
            };
        }

        public static Stock ToStockFromCreateStockDTO(this Dtos.Stock.CreateStockDTO createStockDTO)
        {
            return new Stock
            {
                Sysbol = createStockDTO.Sysbol,
                CompanyName = createStockDTO.CompanyName,
                Purchase = createStockDTO.Purchase,
                LastDiv = createStockDTO.LastDiv,
                Isdustry = createStockDTO.Isdustry,
                MarketCap = createStockDTO.MarketCap
            };
        }
    }
}