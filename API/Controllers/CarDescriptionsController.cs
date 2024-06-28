using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDescriptionsController : ControllerBase
    {
        private readonly ICarDescriptionService _carDescriptionService;
        private readonly ILogger<CarDescriptionsController> _logger;

        public CarDescriptionsController(ICarDescriptionService carDescriptionService, ILogger<CarDescriptionsController> logger)
        {
            _carDescriptionService = carDescriptionService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CarDescriptionList(int id)
        {
            try
            {
                var carDescriptions = _carDescriptionService.TGetCarDescriptionWithCarID(id);
                if (carDescriptions == null )
                {
                    return NotFound("Araba açıklaması bulunamadı");
                }
                return Ok(carDescriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan araba açıklamaları getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
