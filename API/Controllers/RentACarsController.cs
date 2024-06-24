using BusinessLayer.Abstract;
using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentACarsController : ControllerBase
    {
        private readonly IRentACarService _rentACarService;

        public RentACarsController(IRentACarService rentACarService)
        {
            _rentACarService = rentACarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRentACarListByLocation(int locationID, bool available)
        {
            // Filtreyi oluşturan bir Expression oluşturun
            Expression<Func<RentACar, bool>> filter = x => x.LocationID == locationID && x.Available == available;

            // Filtreyi metodunuza geçirin
            var values = await _rentACarService.TGetByFilterAsync(filter);
            return Ok(values);
        }

    }
}
