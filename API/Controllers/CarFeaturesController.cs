using BusinessLayer.Abstract;
using DtoLayer.CarFeatureDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeaturesController : ControllerBase
    {
        private readonly ICarFeatureService _carFeatureService;
        private readonly ILogger<CarFeaturesController> _logger;

        public CarFeaturesController(ICarFeatureService carFeatureService, ILogger<CarFeaturesController> logger)
        {
            _carFeatureService = carFeatureService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarFeaturesByCarId(int carId)
        {
            try
            {
                var result = await _carFeatureService.TGetCarFeaturesByCarID(carId);
                if (result == null || !result.Any())
                {
                    return NotFound("Araç özellikleri bulunamadı");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {carId} olan araç özellikleri getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("CarFeatureChangeAvailableToFalse")]
        public IActionResult CarFeatureChangeAvailableToFalse(int id)
        {
            try
            {
                _carFeatureService.TChangeCarFeatureAvailableToFalse(id);
                return Ok("Güncelleme yapıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan araç özelliğinin durumu güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("CarFeatureChangeAvailableToTrue")]
        public IActionResult CarFeatureChangeAvailableToTrue(int id)
        {
            try
            {
                _carFeatureService.TChangeCarFeatureAvailableToTrue(id);
                return Ok("Güncelleme yapıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan araç özelliğinin durumu güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateCarFeatureByCar(CreateCarFeatureDto createCarFeatureDto)
        {
            try
            {
                _carFeatureService.TCreateCarFeatureByCar(createCarFeatureDto);
                return Ok("Ekleme yapıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araç özelliği eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
