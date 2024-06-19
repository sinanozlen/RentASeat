using BusinessLayer.Abstract;
using DtoLayer.FeatureDtos;
using DtoLayer.ServiceDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _Serviceservice;

        public ServicesController(IServiceService serviceservice)
        {
            _Serviceservice = serviceservice;
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            var Services = _Serviceservice.TGetListAll();
            return Ok(Services);
        }

        [HttpGet("{id}")]
        public IActionResult GetService(int id)
        {
            var service = _Serviceservice.TGetbyID(id);
            if (service == null)
                return NotFound("Hizmet bulunamadı");
            return Ok(service);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var values = _Serviceservice.TGetbyID(id);
            _Serviceservice.TDelete(values);
            return Ok("Hizmet Silme işlemi Başarı ile Gerçekleşti");
        }

        [HttpPost]
        public IActionResult CreateService(CreateServiceDto createServiceDto)
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

        [HttpPut]
        public IActionResult UpdateFeature(UpdateServiceDto updateServiceDto)
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
    }
}
