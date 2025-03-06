using Stocker.Domain.Models;
using Stocker.Infrastructer.Data;
using Stocker.Infrastructer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Stocks = new Repository<Stock>(_context);
            UserStocks = new Repository<UserStock>(_context);
            Users = new Repository<AppUser>(_context);
        }

       
        public IRepository<Stock> Stocks { get; private set; }

        public IRepository<UserStock> UserStocks { get; private set; }
        public IRepository<AppUser> Users { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}