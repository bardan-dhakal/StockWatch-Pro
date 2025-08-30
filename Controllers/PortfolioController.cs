using api.Constants;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IStockRepository stockRepository) : ControllerBase
    {
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IPortfolioRepository portfolioRepository = portfolioRepository;
        private readonly IStockRepository stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }
            var appUser = await userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                return NotFound(ErrorMessages.UserNotFound);
            }
            var portfolios = portfolioRepository.GetStocks(appUser);
            return Ok(portfolios);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId)
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }
            var appUser = await userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                return NotFound(ErrorMessages.UserNotFound);
            }

            if (!await stockRepository.IsStockExist(stockId))
            {
                return NotFound(ErrorMessages.StockNotFound);
            }

            var portfolio = new Portfolio
            {
                StockId = stockId,
                AppUserId = appUser.Id
            };

            var createdPortfolio = await portfolioRepository.CreateAsync(portfolio);
            if (createdPortfolio == null)
            {
                return BadRequest();
            }

            return Ok(createdPortfolio);
        }

        [HttpDelete("{stockId}")]
        public async Task<IActionResult> Delete([FromRoute] int stockId)
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest(ErrorMessages.UserNotFound);
            }

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest(ErrorMessages.UserNotFound);
            }

            var portfolio = new Portfolio
            {
                StockId = stockId,
                AppUserId = user.Id
            };

            var result = await portfolioRepository.DeleteAsync(portfolio);
            if (result == false)
            {
                return BadRequest(result);
            }

            return Ok();

        }
    }
}