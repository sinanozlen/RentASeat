using BusinessLayer.Abstract;
using DtoLayer.AboutDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutsController : ControllerBase
	{
		private readonly IAboutService _aboutService;

		public AboutsController(IAboutService aboutService)
		{
			_aboutService = aboutService;
		}

		[HttpGet]
		public IActionResult AboutList()
		{
			var brands = _aboutService.TGetListAll();
			return Ok(brands);
		}

		[HttpGet("{id}")]
		public IActionResult GetAbout(int id)
		{
			var brand = _aboutService.TGetbyID(id);
			if (brand == null)
				return NotFound("Hakkımızda alanı Bulunamadı");
			return Ok(brand);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteAbout(int id)
		{
			var brand = _aboutService.TGetbyID(id);
			if (brand == null)
				return NotFound("Hakkımızda alanı Bulunamadı");

			_aboutService.TDelete(brand);
			return Ok("Hakkımızda alanı başarı ile Silindi");
		}

		[HttpPost]
		public IActionResult CreateAbout(CreateAboutDto createAboutDto)
		{
			About about = new About
			{
				Description = createAboutDto.Description,
				ImageUrl = createAboutDto.ImageUrl,
				Title = createAboutDto.Title
			};
			_aboutService.TAdd(about);
			return Ok("Hakkımızda Alanı Başarı ile oluşturuldu");
		}

		[HttpPut]

		public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
		{
			var about = _aboutService.TGetbyID(updateAboutDto.AboutId);
			if (about == null)
				return NotFound("Hakkımızda alanı Bulunamadı");




			about.Description = updateAboutDto.Description;
			about.ImageUrl = updateAboutDto.ImageUrl;
			about.Title = updateAboutDto.Title;
			



			_aboutService.TUpdate(about);
			return Ok("Hakkımızda Alanı Başarı ile Güncellendi");
		}
	}

}
