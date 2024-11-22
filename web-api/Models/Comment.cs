using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title {get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }
        // Navigation property, basically as a foreign key to the Stock table
        public Stock Stock { get; set; }

    }
}