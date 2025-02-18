using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.DTOs.Stock;
using web_api.interfaces;
using web_api.Mappers;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context=context;
            _stockRepository=stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stocks = await _stockRepository.GetAllAsync();
            
            if(stocks == null || stocks.Count == 0){
                return NotFound();
            }

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            
            if(stock == null){
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Stock stock)
        {
            var stockProp = await _stockRepository.CreateAsync(stock);

            if(stockProp == null ) return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }

        [HttpPut]
        [Route("{idLint}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO )
        {
            var stockModel = await _stockRepository.UpdateAsync(id,updateDTO);

            if(stockModel == null) return NotFound();


            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete ([FromRoute] int id)
        {
            var stockModel = await _stockRepository.DeleteAsync(id);

            if(stockModel ==  null) return NotFound();


            return NoContent();

        }
        
    }
}