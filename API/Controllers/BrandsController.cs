using BusinessLayer.Abstract;
using DtoLayer.BrandDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(IBrandService brandService, ILogger<BrandsController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult BrandList()
        {
            try
            {
                var brands = _brandService.TGetListAll();
                if (brands == null || !brands.Any())
                {
                    return NotFound("Marka bilgisi bulunamadı");
                }
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marka listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBrand(int id)
        {
            try
            {
                var brand = _brandService.TGetbyID(id);
                if (brand == null)
                {
                    return NotFound("Marka bulunmadı");
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan marka getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                var brand = _brandService.TGetbyID(id);
                if (brand == null)
                {
                    return NotFound("Marka bulunmadı");
                }

                _brandService.TDelete(brand);
                return Ok("Marka silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan marka silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateBrand(CreateBrandDto createBrandDto)
        {
            if (createBrandDto == null || string.IsNullOrWhiteSpace(createBrandDto.name))
            {
                return BadRequest("Geçersiz giriş verileri");
            }

            try
            {
                var brand = new Brand
                {
                    Name = createBrandDto.name
                };
                _brandService.TAdd(brand);
                return StatusCode(StatusCodes.Status201Created, "Marka oluşturma işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marka oluşturulurken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            if (updateBrandDto == null || string.IsNullOrWhiteSpace(updateBrandDto.name))
            {
                return BadRequest("Geçersiz giriş verileri");
            }

            try
            {
                var brand = _brandService.TGetbyID(updateBrandDto.brandID);
                if (brand == null)
                {
                    return NotFound("Marka bulunmadı");
                }

                brand.Name = updateBrandDto.name;
                _brandService.TUpdate(brand);
                return Ok("Marka güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateBrandDto.brandID} olan marka güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
