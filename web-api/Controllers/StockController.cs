using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            var stocks = _context.Stocks.ToList();
            
            if(stocks == null || stocks.Count == 0){
                return NotFound();
            }

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id);
            
            if(stock == null){
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update ([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO )
        {
            var stockModel = _context.Stocks.FirstOrDefault(
                x => x.Id == id
            );

            if(stockModel == null) return NotFound();

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.Purchase= updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;
            stockModel.MarketCap = updateDTO.MarketCap;
            
            _context.SaveChanges();


            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete ([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(obj => obj.Id == id);

            if(stockModel ==  null) return NotFound();
            
            _context.Stocks.Remove(stockModel);

            _context.SaveChanges();

            return NoContent();


        }
        
    }
}