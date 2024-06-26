using BusinessLayer.Abstract;
using DtoLayer.LocationDtos;
using DtoLayer.ReservationDtos;
using DtoLayer.TestimonialDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateTestimonialDto createReservationDto)
        {
            var reservation = new Reservation()
            {
                
                 

                
            };
           
            return Ok("Referans Ekleme İşlemi Başarı İle Gerçekleştirildi");
        }
    }
}
