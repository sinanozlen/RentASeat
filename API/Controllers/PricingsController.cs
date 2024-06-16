using BusinessLayer.Abstract;
using DtoLayer.PricingDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingsController : ControllerBase
    {
        private readonly IPricingService _pricingService;
        public PricingsController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }
        [HttpGet]
        public IActionResult PricingList()
        {
            var pricings = _pricingService.TGetListAll();
            return Ok(pricings);
        }
        [HttpGet("{id}")]
        public IActionResult GetPricing(int id)
        {
            var pricing = _pricingService.TGetbyID(id);
            return Ok(pricing);
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePricing(int id)
        {
            var values = _pricingService.TGetbyID(id);
            _pricingService.TDelete(values);
            return Ok("Ödeme Planı Silme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPost]
        public IActionResult CreatePricing(CreatePricingDto createPricingDto)
        {
            Pricing pricing = new Pricing()
            {
               Name = createPricingDto.Name,
            };
            _pricingService.TAdd(pricing);
            return Ok("Ödeme Planı Ekleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPut]
        public IActionResult UpdatePricing(UpdatePricingDto updatePricingDto)
        {
            Pricing pricing = new Pricing()
            {
                PricingID = updatePricingDto.PricingID,
                Name = updatePricingDto.Name,
            };
            _pricingService.TUpdate(pricing);
            return Ok("Ödeme Planı Güncelleme işlemi Başarı ile Gerçekleşti");
        }
    }
}
