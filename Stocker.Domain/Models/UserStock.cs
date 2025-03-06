using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Domain.Models
{
    public class UserStock
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int ShareCount { get; set; }

        public AppUser User { get; set; }
        public Stock Stock { get; set; }
    }
}
