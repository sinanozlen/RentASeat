using BusinessLayer.Abstract;
using DtoLayer.FooterAddressDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FooterAdressesController : ControllerBase
	{
		private readonly IFooterAddressService _footerAddressService;

		public FooterAdressesController(IFooterAddressService footerAddressService)
		{
			_footerAddressService = footerAddressService;
		}
		[HttpGet]
		public IActionResult FooterAddressList()
		{
			var values = _footerAddressService.TGetListAll();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public IActionResult GetFooterAddress(int id)
		{
			var value = _footerAddressService.TGetbyID(id);
			return Ok(value);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteFooterAddress(int id)
		{
			var value = _footerAddressService.TGetbyID(id);
			_footerAddressService.TDelete(value);
			return Ok("Adres Silme İşlemi Başarı İle Gerçekleşti");
		}
		[HttpPost]
		public IActionResult CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
		{
			var footer = new FooterAddress
			{
				Address = createFooterAddressDto.Address,
				Description = createFooterAddressDto.Description,
				Email = createFooterAddressDto.Email,
				Phone = createFooterAddressDto.Phone
			};
			_footerAddressService.TAdd(footer);
			return Ok("Adres Ekleme İşlemi Başarı İle Gerçekleşti");
		}
		[HttpPut]
		public IActionResult UpdateFooterAddress(UpdateFooterAddressDto updateFooterAddressDto)
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
			return Ok("Adres Güncelleme İşlemi Başarı İle Gerçekleşti");
		}
	}
}
