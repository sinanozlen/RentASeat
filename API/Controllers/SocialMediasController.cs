using BusinessLayer.Abstract;
using DtoLayer.SocialMediaDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly ILogger<SocialMediasController> _logger;

        public SocialMediasController(ISocialMediaService socialMediaService, ILogger<SocialMediasController> logger)
        {
            _socialMediaService = socialMediaService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SocialMediaList()
        {
            try
            {
                var socialMedias = _socialMediaService.TGetListAll();
                return Ok(socialMedias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving social media list");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetSocialMedia(int id)
        {
            try
            {
                var socialMedia = _socialMediaService.TGetbyID(id);
                if (socialMedia == null)
                    return NotFound("Sosyal Medya Bulunamadı");

                return Ok(socialMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving social media with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSocialMedia(int id)
        {
            try
            {
                var socialMedia = _socialMediaService.TGetbyID(id);
                if (socialMedia == null)
                    return NotFound("Sosyal Medya Bulunamadı");

                _socialMediaService.TDelete(socialMedia);
                return Ok("Sosyal Medya başarı ile Silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting social media with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new social media");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating social media");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
