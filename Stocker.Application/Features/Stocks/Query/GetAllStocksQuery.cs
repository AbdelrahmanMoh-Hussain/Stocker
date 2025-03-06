using MediatR;
using Stocker.Domain.Models;
using Stocker.Infrastructer.Helpers;
using Stocker.Infrastructer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Application.Features.Stocks.Query
{
    public record GetAllStocksQuery() : IRequest<IEnumerable<Stock>>;

    public class GetAllStocksQueryHandler : IRequestHandler<GetAllStocksQuery, IEnumerable<Stock>>
    {
        private readonly IStockService _stockService;

        public GetAllStocksQueryHandler(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IEnumerable<Stock>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            return await _stockService.GetAllStocks();
        }
    }
}
