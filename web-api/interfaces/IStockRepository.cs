using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.DTOs.Stock;
using web_api.Models;

namespace web_api.interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock?> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDto);

        Task<Stock?> DeleteAsync(int id);



    }
}