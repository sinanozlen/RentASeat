using BusinessLayer.Abstract;
using DtoLayer.CarDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carservice;

        public CarsController(ICarService carservice)
        {
            _carservice = carservice;
        }

        [HttpGet]
        public IActionResult CarList()
        {
            var cars = _carservice.TGetListAll();
            return Ok(cars);
        }
        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = _carservice.TGetbyID(id);
            return Ok(car);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            var values = _carservice.TGetbyID(id);
            _carservice.TDelete(values);
            return Ok("Araba Silme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPost]
        public IActionResult CreateCar(CreateCarDto createCarDto)
        {
            Car car = new Car()
            {
                BigImageUrl = createCarDto.BigImageUrl,
                BrandID = createCarDto.BrandID,
                CoverImageUrl = createCarDto.CoverImageUrl,
                Km= createCarDto.Km,
                Model = createCarDto.Model,
                Luggage = createCarDto.Luggage,
                Fuel = createCarDto.Fuel,
                Seat = createCarDto.Seat,
                Transmission = createCarDto.Transmission,
                
                
            };
            _carservice.TAdd(car);
            return Ok("Araba Ekleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPut]
        public IActionResult UpdateCar(UpdateCarDto updateCarDto)
        {
            Car car = new Car()
            {
                CarID = updateCarDto.CarId,
                BigImageUrl = updateCarDto.BigImageUrl,
                BrandID = updateCarDto.BrandID,
                CoverImageUrl = updateCarDto.CoverImageUrl,
                Km = updateCarDto.Km,
                Model = updateCarDto.Model,
                Luggage = updateCarDto.Luggage,
                Fuel = updateCarDto.Fuel,
                Seat = updateCarDto.Seat,
                Transmission = updateCarDto.Transmission,
            };
            _carservice.TUpdate(car);
            return Ok("Araba Güncelleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpGet("GetCarsWithBrand")]
        public IActionResult GetCarsWithBrand()
        {
            var cars = _carservice.TGetCarsWithBrand();
            return Ok(cars);
        }

        [HttpGet("Get5CarsWithBrand")]
        public IActionResult Get5CarsWithBrand()
        {
            var values = _carservice.TGet5CarsWithBrandsDtos();
            return Ok(values);
                }
    }
}
