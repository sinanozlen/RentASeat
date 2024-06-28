using BusinessLayer.Abstract;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentACarsController : ControllerBase
    {
        private readonly IRentACarService _rentACarService;
        private readonly ILogger<RentACarsController> _logger;

        public RentACarsController(IRentACarService rentACarService, ILogger<RentACarsController> logger)
        {
            _rentACarService = rentACarService;
            _logger = logger;
        }

        [HttpGet("GetRentACarListByLocation")]
        public async Task<IActionResult> GetRentACarListByLocation(int locationID, bool available)
        {
            try
            {
                // Filtreyi oluşturan bir Expression oluşturun
                Expression<Func<RentACar, bool>> filter = x => x.LocationID == locationID && x.Available == available;

                // Filtreyi metoda geçirin
                var rentACars = await _rentACarService.TGetByFilterAsync(filter);

                if (rentACars == null || !rentACars.Any())
                {
                    _logger.LogWarning($"LocationID: {locationID}, Available: {available} için kiralık araç bulunamadı.");
                    return NotFound("Kiralık araç bulunamadı");
                }
               
                return Ok(rentACars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LocationID: {locationID}, Available: {available} için kiralık araç listesi alınırken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
