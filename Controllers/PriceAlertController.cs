using api.Dtos;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceAlertController : ControllerBase
    {
        private readonly IPriceAlertRepository _priceAlertRepository;

        public PriceAlertController(IPriceAlertRepository priceAlertRepository)
        {
            _priceAlertRepository = priceAlertRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAlerts()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var alerts = await _priceAlertRepository.GetUserAlertsAsync(userId);
            return Ok(alerts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var alert = await _priceAlertRepository.GetByIdAsync(id, userId);
            return alert != null ? Ok(alert) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriceAlertDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            try
            {
                var alert = await _priceAlertRepository.CreateAsync(createDto, userId);
                return CreatedAtAction(nameof(GetById), new { id = alert.Id }, alert);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await _priceAlertRepository.DeleteAsync(id, userId);
            return success ? NoContent() : NotFound();
        }

        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await _priceAlertRepository.ToggleActiveAsync(id, userId);
            return success ? Ok() : NotFound();
        }
    }
}
