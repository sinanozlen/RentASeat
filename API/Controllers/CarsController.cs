using BusinessLayer.Abstract;
using DtoLayer.CarDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CarList()
        {
            try
            {
                var cars = _carService.TGetListAll();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Arabalar getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(int id)
        {
            try
            {
                var car = _carService.TGetbyID(id);
            if (car == null)
            {
                return NotFound("Araba bulunamadı");
            }
            return Ok(car);
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan araba getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
    }
}

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                var car = _carService.TGetbyID(id);
                if (car == null)
                {
                    return NotFound("Araba bulunamadı");
                }

                _carService.TDelete(car);
                return Ok("Araba silme işlemi başarı ile gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan araba silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateCar(CreateCarDto createCarDto)
        {
            try
            {
                Car car = new Car
                {
                    BigImageUrl = createCarDto.BigImageUrl,
                    BrandID = createCarDto.BrandID,
                    CoverImageUrl = createCarDto.CoverImageUrl,
                    Km = createCarDto.Km,
                    Model = createCarDto.Model,
                    Luggage = createCarDto.Luggage,
                    Fuel = createCarDto.Fuel,
                    Seat = createCarDto.Seat,
                    Transmission = createCarDto.Transmission
                };
                _carService.TAdd(car);
                return Ok("Araba ekleme işlemi başarı ile gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Araba eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateCar(UpdateCarDto updateCarDto)
        {
            try
            {
                Car car = new Car
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
                    Transmission = updateCarDto.Transmission
                };
                _carService.TUpdate(car);
                return Ok("Araba güncelleme işlemi başarı ile gerçekleşti");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateCarDto.CarId} olan araba güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("GetCarsWithBrand")]
        public IActionResult GetCarsWithBrand()
        {
            try
            {
                var cars = _carService.TGetCarsWithBrand();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marka bilgisi ile birlikte arabalar getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("Get5CarsWithBrand")]
        public IActionResult Get5CarsWithBrand()
        {
            try
            {
                var cars = _carService.TGet5CarsWithBrandsDtos();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marka bilgisi ile birlikte 5 araba getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
