using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.DTOs.Stock;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stocks = await _context.Stocks.ToListAsync();
            
            if(stocks == null || stocks.Count == 0){
                return NotFound();
            }

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            
            if(stock == null){
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO )
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(
                x => x.Id == id
            );

            if(stockModel == null) return NotFound();

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.Purchase= updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;
            stockModel.MarketCap = updateDTO.MarketCap;
            
           await  _context.SaveChangesAsync();


            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete ([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(obj => obj.Id == id);

            if(stockModel ==  null) return NotFound();
            
            _context.Stocks.Remove(stockModel);

           await _context.SaveChangesAsync();

            return NoContent();

        }
        
    }
}