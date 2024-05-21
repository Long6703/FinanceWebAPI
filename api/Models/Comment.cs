using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? StockId { get; set; }

        public int? StocksId { get; set; }

        public virtual Stock? Stocks { get; set; }
    }
}