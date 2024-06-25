using BusinessLayer.Abstract;
using DtoLayer.SocialMediaDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediasController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }
        [HttpGet]
        public IActionResult SocialMediaList()
        {
            var socialMedias = _socialMediaService.TGetListAll();
            return Ok(socialMedias);
        }
        [HttpGet("{id}")]
        public IActionResult GetSocialMedia(int id)
        {
            var socialMedia = _socialMediaService.TGetbyID(id);
            if (socialMedia == null)
                return NotFound("Sosyal Medya Bulunamadı");
            return Ok(socialMedia);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSocialMedia(int id)
        {
            var socialMedia = _socialMediaService.TGetbyID(id);
            if (socialMedia == null)
                return NotFound("Sosyal Medya Bulunamadı");

            _socialMediaService.TDelete(socialMedia);
            return Ok("Sosyal Medya başarı ile Silindi");
        }
        [HttpPost]
        public IActionResult CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            SocialMedia socialMedia = new SocialMedia
            {
                Icon = createSocialMediaDto.Icon,
                Url = createSocialMediaDto.Url,
                Name = createSocialMediaDto.Name
                
            };
            _socialMediaService.TAdd(socialMedia);
            return Ok("Sosyal Medya Başarı ile oluşturuldu");
        }
        [HttpPut]
        public IActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            SocialMedia socialMedia = new SocialMedia
            {
                SocialMediaID = updateSocialMediaDto.SocialMediaID,
                Icon = updateSocialMediaDto.Icon,
                Url = updateSocialMediaDto.Url,
                Name = updateSocialMediaDto.Name
            };
            _socialMediaService.TUpdate(socialMedia);
            return Ok("Sosyal Medya Başarı ile Güncellendi");
        }
    }
}
