using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;

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
        
    }
}