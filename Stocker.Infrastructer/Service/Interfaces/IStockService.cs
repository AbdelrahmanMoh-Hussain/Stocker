using Stocker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Service.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetAllStocks();
    }
}
