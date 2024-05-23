using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Sysbol { get; set; } = null;

        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; }

        public bool IsDecsending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 1;
    }
}