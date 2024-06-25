using BusinessLayer.Abstract;
using DtoLayer.LocationDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        public IActionResult LocationList()
        {
            var locations = _locationService.TGetListAll();
            return Ok(locations);
        }
        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            var location = _locationService.TGetbyID(id);
            return Ok(location);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var values = _locationService.TGetbyID(id);
            _locationService.TDelete(values);
            return Ok("Konum Silme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPost]
        public IActionResult CreateLocation(CreateLocationDto createLocationDto)
        {
            Location location = new Location()
            {
                Name = createLocationDto.Name,
           
            };
            _locationService.TAdd(location);
            return Ok("Konum Ekleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPut]
        public IActionResult UpdateLocation(UpdatePricingDtpo updateLocationDto)
        {
            Location location = new Location()
            {
                LocationID = updateLocationDto.LocationID,
                Name = updateLocationDto.Name,
            };
            _locationService.TUpdate(location);
            return Ok("Konum Güncelleme işlemi Başarı ile Gerçekleşti");
        }
    }
}
