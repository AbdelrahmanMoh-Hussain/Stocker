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
    public record SellStockCommand(int userStockId) : IRequest<Result>;

    public class SellStockCommandHandler : IRequestHandler<SellStockCommand, Result>
    {
        private readonly IUserStockService _stockService;

        public SellStockCommandHandler(IUserStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Result> Handle(SellStockCommand request, CancellationToken cancellationToken)
        {
            return await _stockService.SellStock(request.userStockId);
        }
    }
}
