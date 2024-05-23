using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel != null)
            {
                _context.Stocks.Remove(stockModel);
                await _context.SaveChangesAsync();
                return stockModel;
            }

            return null;

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stock = _context.Stocks.Include(s => s.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObject.Sysbol))
            {
                stock = stock.Where(s => s.Sysbol.Contains(queryObject.Sysbol));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Sysbol", StringComparison.OrdinalIgnoreCase)){
                    stock = queryObject.IsDecsending ? stock.OrderByDescending(s => s.Id) : stock.OrderBy(s => s.Id);
                }

            }

            var skipRecods = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stock.Skip(skipRecods).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stocks.AnyAsync(e => e.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO updateStockRequestDTO)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel != null)
            {
                stockModel.Sysbol = updateStockRequestDTO.Sysbol;
                stockModel.CompanyName = updateStockRequestDTO.CompanyName;
                stockModel.Purchase = updateStockRequestDTO.Purchase;
                stockModel.LastDiv = updateStockRequestDTO.LastDiv;
                stockModel.Isdustry = updateStockRequestDTO.Isdustry;
                stockModel.MarketCap = updateStockRequestDTO.MarketCap;

                await _context.SaveChangesAsync();
                return stockModel;
            }
            return null;
        }
    }
}