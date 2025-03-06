using MediatR;
using Stocker.Domain.Models;
using Stocker.Infrastructer.Helpers;
using Stocker.Infrastructer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Application.Features.Stocks.Command
{
    public record BuyStockCommand(string logedInUser, string stockCode, int numberOfShares) : IRequest<Result>;

    public class BuyStockCommandHandler : IRequestHandler<BuyStockCommand, Result>
    {
        private readonly IUserStockService _stockService;

        public BuyStockCommandHandler(IUserStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Result> Handle(BuyStockCommand request, CancellationToken cancellationToken)
        {
            return await _stockService.BuyStock(request.logedInUser, request.stockCode, request.numberOfShares);
        }
    }
}
