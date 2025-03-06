using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stocker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            await context.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin", NormalizedName = "ADMIN" });
            }

            if (!await userManager.Users.AnyAsync())
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Balance = 1000m
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (!await context.Stocks.AnyAsync())
            {
                var stocks = new List<Stock>
                {
                    new Stock { Code = "AAPL", CompanyName = "Apple Inc.", PricePerShare = 150.75m },
                    new Stock { Code = "GOOGL", CompanyName = "Alphabet Inc.", PricePerShare = 2800.55m },
                    new Stock { Code = "TSLA", CompanyName = "Tesla Inc.", PricePerShare = 850.40m },
                    new Stock { Code = "AMZN", CompanyName = "Amazon.com Inc.", PricePerShare = 3400.50m },
                    new Stock { Code = "MSFT", CompanyName = "Microsoft Corp.", PricePerShare = 305.60m },
                    new Stock { Code = "FB", CompanyName = "Meta Platforms Inc.", PricePerShare = 360.90m },
                    new Stock { Code = "NFLX", CompanyName = "Netflix Inc.", PricePerShare = 625.30m },
                    new Stock { Code = "NVDA", CompanyName = "NVIDIA Corp.", PricePerShare = 740.45m },
                    new Stock { Code = "INTC", CompanyName = "Intel Corp.", PricePerShare = 55.20m },
                    new Stock {  Code = "AMD", CompanyName = "Advanced Micro Devices", PricePerShare = 110.75m }
                };
                await context.Stocks.AddRangeAsync(stocks);
                await context.SaveChangesAsync();
            }

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser != null && !await context.UserStocks.AnyAsync())
            {
                var userStocks = new List<UserStock>
                {
                    new UserStock { UserId = adminUser.Id, StockId = 1, ShareCount = 10 }, 
                    new UserStock { UserId = adminUser.Id, StockId = 2, ShareCount = 5 },  
                    new UserStock { UserId = adminUser.Id, StockId = 3, ShareCount = 8 },  
                };
                await context.UserStocks.AddRangeAsync(userStocks);
                await context.SaveChangesAsync();
            }
            

        }
    }
}
