using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.DTOs.Stock;
using web_api.interfaces;
using web_api.Models;

namespace web_api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);

            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
           var stockModel  = await _context.Stocks.FirstOrDefaultAsync(_stock => _stock.Id == id);

           if(stockModel == null){
            return null;
           }

           _context.Stocks.Remove(stockModel); //Remove doesn't has a async.\
           await  _context.SaveChangesAsync();

           return stockModel;

        }

        public async Task<List<Stock>> GetAllAsync()
        {

            return await _context.Stocks
                .Include(existComment => existComment.Comments)
                .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await  _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO updateDTO)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(
                _stock => _stock.Id == id
            );

            if(existingStock== null) return null;

           
            existingStock.Symbol = updateDTO.Symbol;
            existingStock.CompanyName = updateDTO.CompanyName;
            existingStock.Purchase= updateDTO.Purchase;
            existingStock.LastDiv = updateDTO.LastDiv;
            existingStock.Industry = updateDTO.Industry;
            existingStock.MarketCap = updateDTO.MarketCap;
            
           await  _context.SaveChangesAsync();

           return existingStock;

        }
    }
}