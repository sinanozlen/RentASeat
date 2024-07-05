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
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult ReservationList()
        {
            var reservations = _reservationService.TGetListAll();
            return Ok(reservations);
        }

        [HttpGet("GetReservationWithLocationAndCar")]
        public IActionResult GetReservationWithLocationAndCar()
        {
            var reservations = _reservationService.TGetReservationWithLocationAndCar();
            return Ok(reservations);
        }
        [HttpGet("GetReservationById")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = _reservationService.TGetReservationDetails(id);
            return Ok(reservation);
        }

        [HttpGet("ChangeReservationStatusToConfirm")]
        public IActionResult ChangeReservationStatusToConfirm(int id)
        {
            _reservationService.TChangeReservationStatusToConfirm(id);
            return Ok("Rezervasyon Durumu Onaylandı");
        }

        [HttpGet("ChangeReservationStatusToDecline")]
        public IActionResult ChangeReservationStatusToDecline(int id)
        {
            _reservationService.TChangeReservationStatusToDecline(id);
            return Ok("Rezervasyon Durumu Reddedildi");
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
           

            return Ok("Rezervasyon Ekleme İşlemi Başarı İle Gerçekleştirildi");


        }
    }
}