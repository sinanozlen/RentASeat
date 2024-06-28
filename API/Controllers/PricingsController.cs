using BusinessLayer.Abstract;
using DtoLayer.PricingDtos;
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
    public class PricingsController : ControllerBase
    {
        private readonly IPricingService _pricingService;
        private readonly ILogger<PricingsController> _logger;

        public PricingsController(IPricingService pricingService, ILogger<PricingsController> logger)
        {
            _pricingService = pricingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult PricingList()
        {
            try
            {
                var pricings = _pricingService.TGetListAll();
                return Ok(pricings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme planları listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPricing(int id)
        {
            try
            {
                var pricing = _pricingService.TGetbyID(id);
                if (pricing == null)
                {
                    return NotFound("Ödeme planı bulunamadı");
                }
                return Ok(pricing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan ödeme planı getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePricing(int id)
        {
            try
            {
                var pricing = _pricingService.TGetbyID(id);
                if (pricing == null)
                {
                    return NotFound("Ödeme planı bulunamadı");
                }

                _pricingService.TDelete(pricing);
                return Ok("Ödeme planı silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan ödeme planı silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreatePricing(CreatePricingDto createPricingDto)
        {
            try
            {
                var pricing = new Pricing()
                {
                    Name = createPricingDto.Name
                };

                _pricingService.TAdd(pricing);
                return Ok("Ödeme planı ekleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme planı eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdatePricing(UpdatePricingDto updatePricingDto)
        {
            try
            {
                var pricing = new Pricing()
                {
                    PricingID = updatePricingDto.PricingID,
                    Name = updatePricingDto.Name
                };

                _pricingService.TUpdate(pricing);
                return Ok("Ödeme planı güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updatePricingDto.PricingID} olan ödeme planı güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
