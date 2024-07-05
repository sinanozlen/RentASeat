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
