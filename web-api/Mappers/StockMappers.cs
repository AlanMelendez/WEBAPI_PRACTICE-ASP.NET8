using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.DTOs.Stock;
using web_api.Models;

namespace web_api.Mappers
{
    public static class StockMappers
    {

        //This is a method that converts a StockDTO to a Stock model (A mapper method manually created)
        public static StockDTO ToStockDto(this Stock stockModel)
        {
            return new StockDTO{

                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments =  stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
    }
}