using BusinessLayer.Abstract;
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
        [HttpGet("getcarfeaturesbycarid")]
        public async Task<IActionResult> CarFeatureListByCarId(int carID)
        {
            var result = await _carFeatureService.TGetCarFeaturesByCarID(carID);
            return Ok(result);
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
        public IActionResult CreateCarFeatureByCar(CarFeature carFeature)
        {
            _carFeatureService.TCreateCarFeatureByCar(carFeature);
            return Ok("Ekleme Yapıldı");
        }
    }
}
