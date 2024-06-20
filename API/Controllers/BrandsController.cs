using BusinessLayer.Abstract;
using DtoLayer.BrandDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult BrandList()
        {
            var brands = _brandService.TGetListAll();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public IActionResult GetBrand(int id)
        {
            var brand = _brandService.TGetbyID(id);
            if (brand == null)
                return NotFound("Marka bulunmadı");
            return Ok(brand);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _brandService.TGetbyID(id);
            if (brand == null)
                return NotFound("Marka bulunmadı");

            _brandService.TDelete(brand);
            return Ok("Marka Silme İşlemi Başarı ile Gerçekleştirildi");
        }

        [HttpPost]
        public IActionResult CreateBrand(CreateBrandDto createBrandDto)
        {
            Brand brand = new Brand
            {
                Name = createBrandDto.name
            };
            _brandService.TAdd(brand);
            return Ok("Marka Oluşturma İşlemi Başarı ile Gerçekleştirildi");
        }

        [HttpPut]

        public IActionResult UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var brand = _brandService.TGetbyID(updateBrandDto.brandID);
            if (brand == null)
                return NotFound("Marka bulunmadı");

            brand.Name = updateBrandDto.name;
            _brandService.TUpdate(brand);
            return Ok("Marka Güncelleme İşlemi Başarı ile Gerçekleştirildi");
        }
    }
}
