using DtoLayer.LocationDtos;
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
        public async Task<IActionResult> Index()
        {
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
            return View();
        }
    }
}
