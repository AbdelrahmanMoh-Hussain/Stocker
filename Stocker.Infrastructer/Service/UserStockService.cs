using Microsoft.AspNetCore.Identity;
using Stocker.Domain.Models;
using Stocker.Infrastructer.Helpers;
using Stocker.Infrastructer.Repository.Interfaces;
using Stocker.Infrastructer.Service.Interfaces;

namespace Stocker.Infrastructer.Service
{
    public class UserStockService : IUserStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public UserStockService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<Result<IEnumerable<UserStock>>> GetMyStocks(string logedInUser = "admin")
        {
            var user = await _userManager.FindByNameAsync(logedInUser);
            if (user == null)
                return Result.Fail<IEnumerable<UserStock>>("User is not authenticated");

            var userStocks = (await _unitOfWork.UserStocks.GetWhereWithIncludeAsync(u => u.UserId == user.Id, ["Stock"]));
            return Result.Success(userStocks);

        }
        public async Task<Result> BuyStock(string logedInUser, string stockCode, int numberOfShares)
        {
            var stock = (await _unitOfWork.Stocks.Find(s => s.Code == stockCode));
            if (stock == null)
                return Result.Fail("This Stock does not exist");

            var user = await _userManager.FindByNameAsync(logedInUser);
            if (user == null)
                return Result.Fail<IEnumerable<Stock>>("User is not authenticated");

            if (user.Balance - (numberOfShares * stock.PricePerShare) < 0)
                return Result.Fail("Insufficient balance");

            user.Balance -= numberOfShares * stock.PricePerShare;

            if(await _unitOfWork.UserStocks.IsExist(us => us.UserId == user.Id && us.StockId == stock.Id))
            {
                var userStock = await _unitOfWork.UserStocks.Find(us => us.UserId == user.Id && us.StockId == stock.Id);
                userStock.ShareCount += numberOfShares;
            }
            else
            {
                await _unitOfWork.UserStocks.AddAsync(new UserStock
                {
                    UserId = user.Id,
                    StockId = stock.Id,
                    ShareCount = numberOfShares
                });
            }
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

        public async Task<Result> DecreaseShare(int userStockId, int decreaseBy)
        {
            var userStock = await _unitOfWork.UserStocks.Find(u => u.Id == userStockId, ["Stock", "User"]);
            if (userStock == null)
                return Result.Fail($"User not has this stock");

            if (userStock.ShareCount - decreaseBy < 0)
                return Result.Fail("No sufficient shares");

            userStock.User.Balance += (userStock.Stock.PricePerShare * decreaseBy);
            userStock.ShareCount -= decreaseBy;
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

        public async Task<Result> IncreaseShare(int userStockId, int increaseBy)
        {
            var userStock = await _unitOfWork.UserStocks.Find(u => u.Id == userStockId, ["Stock", "User"]);
            if (userStock == null)
                return Result.Fail($"User not has this stock");

            if (userStock.User.Balance - (userStock.Stock.PricePerShare * increaseBy) < 0)
                return Result.Fail("Insufficient balance");

            userStock.User.Balance -= (userStock.Stock.PricePerShare * increaseBy);
            userStock.ShareCount += increaseBy;
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

        public async Task<Result> SellStock(int userStockId)
        {
            var userStock = await _unitOfWork.UserStocks.Find(u => u.Id == userStockId, ["Stock", "User"]);

            if (userStock == null)
                return Result.Fail($"User not has this stock");

            userStock.User.Balance += (userStock.ShareCount * userStock.Stock.PricePerShare);
            _unitOfWork.UserStocks.Delete(userStock); //Better to add sold column to the database instead of deleting
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

    }
}
