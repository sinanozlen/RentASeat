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
    [Authorize(Roles = Roles.Admin + "," + Roles.Manager)]
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
                return NotFound("Brand not found");
            return Ok(brand);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteBrand(int id)
        {
            var brand = _brandService.TGetbyID(id);
            if (brand == null)
                return NotFound("Brand not found");

            _brandService.TDelete(brand);
            return Ok("Brand deleted successfully");
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult CreateBrand(CreateBrandDto createBrandDto)
        {
            Brand brand = new Brand
            {
                Name = createBrandDto.name
            };
            _brandService.TAdd(brand);
            return Ok("Brand created successfully");
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var brand = _brandService.TGetbyID(updateBrandDto.brandID);
            if (brand == null)
                return NotFound("Brand not found");

            brand.Name = updateBrandDto.name;
            _brandService.TUpdate(brand);
            return Ok("Brand updated successfully");
        }
    }
}
