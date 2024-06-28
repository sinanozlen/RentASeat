using BusinessLayer.Abstract;
using DtoLayer.AboutDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly ILogger<AboutsController> _logger;

        public AboutsController(IAboutService aboutService, ILogger<AboutsController> logger)
        {
            _aboutService = aboutService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            try
            {
                var brands = _aboutService.TGetListAll();
                if (brands == null || !brands.Any())
                {
                    return NotFound("Hakkımızda bilgisi bulunamadı");
                }
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hakkımızda listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAbout(int id)
        {
            try
            {
                var brand = _aboutService.TGetbyID(id);
                if (brand == null)
                {
                    return NotFound("Hakkımızda alanı bulunamadı");
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan Hakkımızda alanı getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id)
        {
            try
            {
                var brand = _aboutService.TGetbyID(id);
                if (brand == null)
                {
                    return NotFound("Hakkımızda alanı bulunamadı");
                }

                _aboutService.TDelete(brand);
                return Ok("Hakkımızda alanı başarı ile silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan Hakkımızda alanı silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            if (createAboutDto == null || string.IsNullOrWhiteSpace(createAboutDto.Description) || string.IsNullOrWhiteSpace(createAboutDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri");
            }

            try
            {
                About about = new About
                {
                    Description = createAboutDto.Description,
                    ImageUrl = createAboutDto.ImageUrl,
                    Title = createAboutDto.Title
                };
                _aboutService.TAdd(about);
                return StatusCode(StatusCodes.Status201Created, "Hakkımızda alanı başarı ile oluşturuldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hakkımızda alanı oluşturulurken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            if (updateAboutDto == null || string.IsNullOrWhiteSpace(updateAboutDto.Description) || string.IsNullOrWhiteSpace(updateAboutDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri");
            }

            try
            {
                var about = _aboutService.TGetbyID(updateAboutDto.AboutId);
                if (about == null)
                {
                    return NotFound("Hakkımızda alanı bulunamadı");
                }

                about.Description = updateAboutDto.Description;
                about.ImageUrl = updateAboutDto.ImageUrl;
                about.Title = updateAboutDto.Title;

                _aboutService.TUpdate(about);
                return Ok("Hakkımızda alanı başarı ile güncellendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateAboutDto.AboutId} olan Hakkımızda alanı güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
