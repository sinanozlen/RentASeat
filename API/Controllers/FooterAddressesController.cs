using BusinessLayer.Abstract;
using DtoLayer.FooterAddressDtos;
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
    public class FooterAddressesController : ControllerBase
    {
        private readonly IFooterAddressService _footerAddressService;
        private readonly ILogger<FooterAddressesController> _logger;

        public FooterAddressesController(IFooterAddressService footerAddressService, ILogger<FooterAddressesController> logger)
        {
            _footerAddressService = footerAddressService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FooterAddressList()
        {
            try
            {
                var values = _footerAddressService.TGetListAll();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer adres listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFooterAddress(int id)
        {
            try
            {
                var value = _footerAddressService.TGetbyID(id);
                if (value == null)
                {
                    return NotFound("Footer adres bulunamadı");
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan footer adres getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFooterAddress(int id)
        {
            try
            {
                var value = _footerAddressService.TGetbyID(id);
                if (value == null)
                {
                    return NotFound("Footer adres bulunamadı");
                }

                _footerAddressService.TDelete(value);
                return Ok("Footer adres silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan footer adres silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
        {
            try
            {
                var footer = new FooterAddress
                {
                    Address = createFooterAddressDto.Address,
                    Description = createFooterAddressDto.Description,
                    Email = createFooterAddressDto.Email,
                    Phone = createFooterAddressDto.Phone
                };

                _footerAddressService.TAdd(footer);
                return Ok("Footer adres ekleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer adres eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateFooterAddress(UpdateFooterAddressDto updateFooterAddressDto)
        {
            try
            {
                var footer = new FooterAddress
                {
                    FooterAddressID = updateFooterAddressDto.FooterAddressID,
                    Address = updateFooterAddressDto.Address,
                    Description = updateFooterAddressDto.Description,
                    Email = updateFooterAddressDto.Email,
                    Phone = updateFooterAddressDto.Phone
                };

                _footerAddressService.TUpdate(footer);
                return Ok("Footer adres güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateFooterAddressDto.FooterAddressID} olan footer adres güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
