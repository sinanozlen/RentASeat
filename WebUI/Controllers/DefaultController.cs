using DtoLayer.LocationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebUI.Controllers
{
    public class DefaultController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            // Kullanıcı claim'lerini kontrol edin
            var userClaims = User.Claims.ToList();
            Console.WriteLine("User claims in HttpGet method:");
            foreach (var claim in userClaims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // Kullanıcı claim'lerinden token'ı alın
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            Console.WriteLine($"Retrieved Token: {token}"); // Token'ı kontrol edin

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                // Authorization başlığına token'ı ekleyin
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("https://localhost:7250/api/Locations");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(json);

                    var valuesitem = values.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.LocationID.ToString()
                    }).ToList();

                    ViewBag.v = valuesitem;
                }
                else
                {
                    ModelState.AddModelError("", "Lokasyonları getirme başarısız oldu");
                }
            }
            else
            {
                ModelState.AddModelError("", "Yetkilendirme token'ı bulunamadı");
            }

            return View();
        }



        [HttpPost]
        public IActionResult Index(string book_pick_date, string book_off_date, string time_pick, string time_off, string locationID)
        {
            TempData["bookpickdate"] = book_pick_date;
            TempData["bookoffdate"] = book_off_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["locationID"] = locationID;
            return RedirectToAction("Index", "RentACarList");
        }
    }
}
