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
    public record IncreaseShareCommand(int userStockId, int increaseBy) : IRequest<Result>;

    public class IncreaseShareCommandHandler : IRequestHandler<IncreaseShareCommand, Result>
    {
        private readonly IUserStockService _stockService;

        public IncreaseShareCommandHandler(IUserStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<Result> Handle(IncreaseShareCommand request, CancellationToken cancellationToken)
        {
            return await _stockService.IncreaseShare(request.userStockId, request.increaseBy);
        }
    }
}
