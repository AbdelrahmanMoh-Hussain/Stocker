using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Domain.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal PricePerShare { get; set; }

        public ICollection<UserStock> UserStocks { get; set; } = new List<UserStock>();

    }
}
