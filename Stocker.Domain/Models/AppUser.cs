using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Domain.Models
{
    public class AppUser : IdentityUser<int>
    {
        public decimal Balance { get; set; } = 1000m;
        public ICollection<UserStock> UserStocks { get; set; } = new List<UserStock>();
    }
}
