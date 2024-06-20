using BusinessLayer.Abstract;
using DtoLayer.BannerDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BannersController : ControllerBase
	{
		private readonly IBannerService _bannerService;

		public BannersController(IBannerService bannerService)
		{
			_bannerService = bannerService;
		}
		[HttpGet]
		public IActionResult BannerList()
		{
			var banners = _bannerService.TGetListAll();
			return Ok(banners);
		}
		[HttpGet("{id}")]
		public IActionResult GetBanner(int id)
		{
			var banner = _bannerService.TGetbyID(id);
			if (banner == null)
			{
				return NotFound();
			}
			return Ok(banner);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteBanner(int id)
		{
			var banner = _bannerService.TGetbyID(id);
			if (banner == null)
			{
				return NotFound();
			}
			_bannerService.TDelete(banner);
			return Ok("Banner Silme İşlemi Başarı ile Gerçekleştirildi");
		}
		[HttpPost]
		public IActionResult CreateBanner(CreateBannerDto createBannerDto)
		{
			var banner = new Banner()
			{
				Description = createBannerDto.Description,
				VideoDescription = createBannerDto.VideoDescription,
				Title = createBannerDto.Title,
				VideoUrl = createBannerDto.VideoUrl,
			};
			_bannerService.TAdd(banner);
			return Ok("Banner Oluşturma İşlemi Başarı ile Gerçekleştirildi");
		}
		[HttpPut]
		public IActionResult UpdateBanner(UpdateBannerDto updateBannerDto)
		{
			var banner = _bannerService.TGetbyID(updateBannerDto.BannerID);
			if (banner == null)
			{
				return NotFound();
			}
			banner.Description = updateBannerDto.Description;
			banner.VideoDescription = updateBannerDto.VideoDescription;
			banner.Title = updateBannerDto.Title;
			banner.VideoUrl = updateBannerDto.VideoUrl;
			banner.BannerID = updateBannerDto.BannerID;
			_bannerService.TUpdate(banner);
			return Ok("Banner Güncelleme İşlemi Başarı ile Gerçekleştirildi");
		}
	}
}
