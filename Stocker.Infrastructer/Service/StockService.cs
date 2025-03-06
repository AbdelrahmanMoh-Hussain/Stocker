using Stocker.Domain.Models;
using Stocker.Infrastructer.Repository.Interfaces;
using Stocker.Infrastructer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Service
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            return await _unitOfWork.Stocks.GetAllAsync();
        }
    }
}
