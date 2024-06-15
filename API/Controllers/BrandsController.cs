using BusinessLayer.Abstract;
using DtoLayer.BrandDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(brand);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            var values = _brandService.TGetbyID(id);
            _brandService.TDelete(values);
            return Ok("Marka Silme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPost]
        public IActionResult CreateBrand(CreateBrandDto createBrandDto)
        {
            Brand brand = new Brand()
            {
                Name = createBrandDto.name

            };
            _brandService.TAdd(brand);
            return Ok("Marka Ekleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPut]
        public IActionResult UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            Brand brand = new Brand()
            {
                BrandID = updateBrandDto.brandID,
                Name = updateBrandDto.name
            };
            _brandService.TUpdate(brand);
            return Ok("Marka Güncelleme işlemi Başarı ile Gerçekleşti");
        }
    }
}
