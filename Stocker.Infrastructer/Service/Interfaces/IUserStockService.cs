using Stocker.Domain.Models;
using Stocker.Infrastructer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Infrastructer.Service.Interfaces
{
    public interface IUserStockService
    {
        Task<Result<IEnumerable<UserStock>>> GetMyStocks(string logedInUser);
        Task<Result> BuyStock(string logedInUser, string stockCode, int numberOfShares);
        Task<Result> SellStock(int userStockId);
        Task<Result> IncreaseShare(int userStockId, int increaseBy);
        Task<Result> DecreaseShare(int userStockId, int decreaseBy);
    }
}
