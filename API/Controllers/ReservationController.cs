using BusinessLayer.Abstract;
using DtoLayer.LocationDtos;
using DtoLayer.ReservationDtos;
using DtoLayer.TestimonialDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Numerics;

namespace WebUI.Controllers

    
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation()
            {

                Age = createReservationDto.Age,
                CarID = createReservationDto.CarID,
                Description = createReservationDto.Description,
                Name = createReservationDto.Name,
                Surname = createReservationDto.Surname,
                Email = createReservationDto.Email,
                Phone = createReservationDto.Phone,
                PickUpLocationID = createReservationDto.PickUpLocationID,
                DropOffLocationID = createReservationDto.DropOffLocationID,
                DriverLicenseYear = createReservationDto.DriverLicenseYear,
                Status = createReservationDto.Status
                
                






            };
           _reservationService.TAdd(reservation);
           

            return Ok("Referans Ekleme İşlemi Başarı İle Gerçekleştirildi");


        }
    }
}