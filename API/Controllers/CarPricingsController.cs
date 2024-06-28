using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPricingsController : ControllerBase
    {
        private readonly ICarPricingService _carPricingService;
        private readonly ILogger<CarPricingsController> _logger;

        public CarPricingsController(ICarPricingService carPricingService, ILogger<CarPricingsController> logger)
        {
            _carPricingService = carPricingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CarPricingWithCarList()
        {
            try
            {
                var carPricings = _carPricingService.TGetCarPricingWithCars();
                if (carPricings == null || !carPricings.Any())
                {
                    return NotFound("Araç fiyatlandırmaları bulunamadı");
                }
                return Ok(carPricings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç fiyatlandırmaları getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("CarPricingListWithModel")]
        public IActionResult CarPricingListWithModel()
        {
            try
            {
                var carPricings = _carPricingService.TGetCarPricingListWithModel();
                if (carPricings == null || !carPricings.Any())
                {
                    return NotFound("Araç fiyatlandırmaları model bilgisi ile birlikte bulunamadı");
                }
                return Ok(carPricings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç fiyatlandırmaları model bilgisi ile birlikte getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
