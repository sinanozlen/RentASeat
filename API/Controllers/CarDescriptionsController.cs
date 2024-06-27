using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDescriptionsController : ControllerBase
    {
        private readonly ICarDescriptionService _carDescriptionService;

        public CarDescriptionsController(ICarDescriptionService carDescriptionService)
        {
            _carDescriptionService = carDescriptionService;
        }

        [HttpGet]
        public IActionResult CarDescriptionList(int id)
        {
            var carDescriptions = _carDescriptionService.TGetCarDescriptionWithCarID(id);
            return Ok(carDescriptions);
        }


    }
}
