using BusinessLayer.Abstract;
using DtoLayer.LocationDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILocationService locationService, ILogger<LocationsController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult LocationList()
        {
            try
            {
                var locations = _locationService.TGetListAll();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Konum listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLocation(int id)
        {
            try
            {
                var location = _locationService.TGetbyID(id);
                if (location == null)
                {
                    return NotFound("Konum bulunamadı");
                }
                return Ok(location);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan konum getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            try
            {
                var location = _locationService.TGetbyID(id);
                if (location == null)
                {
                    return NotFound("Konum bulunamadı");
                }

                _locationService.TDelete(location);
                return Ok("Konum silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan konum silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateLocation(CreateLocationDto createLocationDto)
        {
            try
            {
                var location = new Location()
                {
                    Name = createLocationDto.Name
                };

                _locationService.TAdd(location);
                return Ok("Konum ekleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Konum eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateLocation(UpdateLocationDto updateLocationDto)
        {
            try
            {
                var location = new Location()
                {
                    LocationID = updateLocationDto.LocationID,
                    Name = updateLocationDto.Name
                };

                _locationService.TUpdate(location);
                return Ok("Konum güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateLocationDto.LocationID} olan konum güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
