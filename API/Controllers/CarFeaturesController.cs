using BusinessLayer.Abstract;
using DtoLayer.CarFeatureDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeaturesController : ControllerBase
    {
        private readonly ICarFeatureService _carFeatureService;
        public CarFeaturesController(ICarFeatureService carFeatureService)
        {
            _carFeatureService = carFeatureService;
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

                return StatusCode(500, "Bir hata oluştu");
            }
        }
        [HttpGet("CarFeatureChangeAvailableToFalse")]
        public IActionResult CarFeatureChangeAvailableToFalse(int id)
        {
            _carFeatureService.TChangeCarFeatureAvailableToFalse(id);
            return Ok("Güncelleme Yapıldı");
        }
        [HttpGet("CarFeatureChangeAvailableToTrue")]
        public IActionResult CarFeatureChangeAvailableToTrue(int id)
        {
            _carFeatureService.TChangeCarFeatureAvailableToTrue(id);
            return Ok("Güncelleme Yapıldı");
        }
        [HttpPost]
        public IActionResult CreateCarFeatureByCar(CreateCarFeatureDto createCarFeatureDto)
        {
            _carFeatureService.TCreateCarFeatureByCar(createCarFeatureDto);
            return Ok("Ekleme Yapıldı");
        }
    }
}
