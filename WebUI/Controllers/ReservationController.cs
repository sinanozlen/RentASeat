using DtoLayer.CarDtos;
using DtoLayer.LocationDtos;
using DtoLayer.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.carid = id;
            ViewBag.v1 = "Araç Kiralama";
            ViewBag.v2 = "Araç Rezervasyon Formu";

            var client = _httpClientFactory.CreateClient();



            var response = await client.GetAsync("https://localhost:7250/api/Locations");


            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(json);
                List<SelectListItem> valuesitem = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.LocationID.ToString()

                                                   }).ToList();
                ViewBag.v = valuesitem;
            }
            var response1 = await client.GetAsync("https://localhost:7250/api/Cars/GetCarsWithBrand");


            if (response1.IsSuccessStatusCode)
            {
                var json = await response1.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandDto>>(json);
                List<SelectListItem> valuesitem = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.BrandName+" "+ x.Model,
                                                       Value = x.CarID.ToString()

                                                   }).ToList();
                ViewBag.v3 = valuesitem;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, CreateReservationDto createReservationDto)
        {

            createReservationDto.CarID = id;
            createReservationDto.Status = "Onay Bekliyor";
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Reservations", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }


    }
    }

