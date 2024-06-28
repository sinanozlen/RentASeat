using BusinessLayer.Abstract;
using DtoLayer.FeatureDtos;
using DtoLayer.ServiceDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _Serviceservice;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceService serviceservice, ILogger<ServicesController> logger)
        {
            _Serviceservice = serviceservice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            try
            {
                var services = _Serviceservice.TGetListAll();
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service list");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            try
            {
                var service = _Serviceservice.TGetbyID(id);
                if (service == null)
                    return NotFound("Hizmet bulunamadı");

                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            try
            {
                var service = _Serviceservice.TGetbyID(id);
                if (service == null)
                    return NotFound("Hizmet bulunamadı");

                _Serviceservice.TDelete(service);
                return Ok("Hizmet Silme işlemi Başarı ile Gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting service with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateService(CreateServiceDto createServiceDto)
        {
            try
            {
                Service service = new Service()
                {
                    Description = createServiceDto.Description,
                    IconUrl = createServiceDto.IconUrl,
                    Title = createServiceDto.Title
                };

                _Serviceservice.TAdd(service);
                return Ok("Hizmet Ekleme işlemi Başarı ile Gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new service");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateServiceDto updateServiceDto)
        {
            try
            {
                Service service = new Service()
                {
                    Description = updateServiceDto.Description,
                    IconUrl = updateServiceDto.IconUrl,
                    Title = updateServiceDto.Title,
                };

                _Serviceservice.TUpdate(service);
                return Ok("Hizmet Güncelleme işlemi Başarı ile Gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
