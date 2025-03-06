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
    public record GetAllUserStocksQuery(string logedInUser) : IRequest<Result<IEnumerable<UserStock>>>;

    public class GetAllUserStocksQueryHandler : IRequestHandler<GetAllUserStocksQuery, Result<IEnumerable<UserStock>>>
    {
        private readonly IUserStockService _stockService;

        public GetAllUserStocksQueryHandler(IUserStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Result<IEnumerable<UserStock>>> Handle(GetAllUserStocksQuery request, CancellationToken cancellationToken)
        {
            return await _stockService.GetMyStocks(request.logedInUser);
        }
    }
}
