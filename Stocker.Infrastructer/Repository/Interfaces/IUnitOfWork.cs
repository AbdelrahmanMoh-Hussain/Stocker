using Stocker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Stock> Stocks { get;  }
        IRepository<UserStock> UserStocks { get;  }
        IRepository<AppUser> Users { get;  }

        Task SaveAsync();
    }
}
