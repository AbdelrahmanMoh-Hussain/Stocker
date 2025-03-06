using MediatR;
using Stocker.Infrastructer.Helpers;
using Stocker.Infrastructer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Application.Features.Stocks.Command
{
    public record DecreaseShareCommand(int userStockId, int decreaseBy) : IRequest<Result>;

    public class DecreaseShareCommandHandler : IRequestHandler<DecreaseShareCommand, Result>
    {
        private readonly IUserStockService _stockService;

        public DecreaseShareCommandHandler(IUserStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Result> Handle(DecreaseShareCommand request, CancellationToken cancellationToken)
        {
            return await _stockService.DecreaseShare(request.userStockId, request.decreaseBy);
        }
    }
}
