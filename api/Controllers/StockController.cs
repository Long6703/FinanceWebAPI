using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interface;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ApplicationDbContext _context;

        public StockController(IStockRepository stockRepository, ApplicationDbContext context)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stockdto = stocks.Select(s => s.ToStockDTO());
            return Ok(stockdto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO createStockDTO){
            var stock = createStockDTO.ToStockFromCreateStockDTO();
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStockRequestDTO)
        {
            var stock =await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Sysbol = updateStockRequestDTO.Sysbol;
            stock.CompanyName = updateStockRequestDTO.CompanyName;
            stock.Purchase = updateStockRequestDTO.Purchase;
            stock.LastDiv = updateStockRequestDTO.LastDiv;
            stock.Isdustry = updateStockRequestDTO.Isdustry;
            stock.MarketCap = updateStockRequestDTO.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stock.ToStockDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    } 
}