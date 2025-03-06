using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stocker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts)
        {

        }
    }
}
