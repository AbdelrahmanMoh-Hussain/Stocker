using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stocker.Application.Features.Stocks.Command;
using Stocker.Application.Features.Stocks.Query;
using Stocker.Web.ViewModels;

namespace Stocker.Web.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        
        {
            var result = await _mediator.Send(new GetAllUserStocksQuery(User.Identity.Name ?? "admin"));
            if (!result.IsSuccess)
                return Json(new {success = false, error = result.Error});

            return View(result.Value);
        }

        public async Task<IActionResult> BuyStock()
        {
            BuyStockViewModel buyStockViewModel = new BuyStockViewModel
            {
                StocksList = (await _mediator.Send(new GetAllStocksQuery())).Select(s => $"{s.Code}-{s.CompanyName} ({s.PricePerShare.ToString("C")})")
            };
            return View(buyStockViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> BuyStock(BuyStockViewModel model)
        {
            ModelState.Remove("StocksList"); // Remove StockList from validation
            if (!ModelState.IsValid)
            {
                BuyStockViewModel buyStockViewModel = new BuyStockViewModel
                {
                    StocksList = (await _mediator.Send(new GetAllStocksQuery())).Select(s => $"{s.Code}-{s.CompanyName} ({s.PricePerShare.ToString("C")})")
                };
                return View(buyStockViewModel);
            }

            var result = await _mediator.Send(new BuyStockCommand(User.Identity.Name, model.Stock, model.SharesCount));
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error);
                BuyStockViewModel buyStockViewModel = new BuyStockViewModel
                {
                    StocksList = (await _mediator.Send(new GetAllStocksQuery())).Select(s => $"{s.Code}-{s.CompanyName} ({s.PricePerShare.ToString("C")})")
                };
                return View(buyStockViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseShare(int userStockId, int increaseBy)
        {
            var result = await _mediator.Send(new IncreaseShareCommand(userStockId, increaseBy));
            if (!result.IsSuccess)
                return Json(new { success = false, error = result.Error });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseShare(int userStockId, int decreaseBy)
        {
            var result = await _mediator.Send(new DecreaseShareCommand(userStockId, decreaseBy));
            if (!result.IsSuccess)
                return Json(new { success = false, error = result.Error });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> SellStock(int userStockId)
        {
            var result = await _mediator.Send(new SellStockCommand(userStockId));
            if (!result.IsSuccess)
                return Json(new { success = false, error = result.Error });

            return Json(new { success = true });
        }

    }
}
